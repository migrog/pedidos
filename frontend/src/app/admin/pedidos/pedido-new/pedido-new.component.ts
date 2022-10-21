import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, PRIMARY_OUTLET, Router, UrlSegment, UrlSegmentGroup, UrlTree } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { catchError, debounceTime, distinctUntilChanged, map, Observable, of, OperatorFunction, switchMap, tap } from 'rxjs';
import { ProductoAddComponent } from 'src/app/shared/componentes/producto-add/producto-add.component';
import { IClienteSearchResponse } from 'src/app/shared/modelos/interfaces/ICliente';
import { IPedidoDetallePost, IPedidoDetallePut, IPedidoPost, IPedidoPostRequest, IPedidoPut, IPedidoPutRequest, IProductoAdd } from 'src/app/shared/modelos/interfaces/IPedido';
import { ClienteService } from 'src/app/shared/services/cliente.service';
import { PedidoService } from 'src/app/shared/services/pedido.service';
import { AlertaService } from 'src/app/utils/alerta.service';
import { AccionEnum } from 'src/app/utils/enums';

@Component({
  selector: 'app-pedido-new',
  templateUrl: './pedido-new.component.html',
  styleUrls: ['./pedido-new.component.css']
})
export class PedidoNewComponent implements OnInit {

  //cliente
  cliente!: IClienteSearchResponse;
  clienteSelected!: IClienteSearchResponse;

  formatter = (cliente: IClienteSearchResponse) => cliente.nombre
  searchingCliente = false;
  searchClienteFailed = false;

  pedidoGroup!: FormGroup;
  detalle: IProductoAdd[] = [];
  total:number = 0;
  idPedido: number = 0;
  accion: AccionEnum = AccionEnum.NUEVO

  loading: boolean = false;
  loading_detalle: boolean = false;

  tree!: UrlTree;
  g!: UrlSegmentGroup;
  s!: UrlSegment[];

  constructor(private router: Router, private route: ActivatedRoute, private fb: FormBuilder, private pipe: DatePipe, private modalService: NgbModal, public alertaService: AlertaService, private clienteService: ClienteService, private pedidoService: PedidoService) { }

  get f() { return this.pedidoGroup.controls; }

  ngOnInit(){
    this.pedidoGroup = this.fb.group(
      {
        id: [''],
        idCliente:[''],
        monedaEnum: ['1', Validators.required],
        fechaEmision:['', Validators.required]
      }
    );

    this.tree = this.router.parseUrl(this.router.url);
    this.g = this.tree.root.children[PRIMARY_OUTLET];
    this.s = this.g.segments;

    if(this.s.length >= 1){
      if(this.s[1].path == 'edit'){
        const id = Number(this.route.snapshot.paramMap.get('id'));
        if(isNaN(id)){
          this.router.navigate(['/pedidos'], { replaceUrl: true });
          return
        }
        this.idPedido = id;
        this.accion = AccionEnum.EDITAR
        this.cargarPedido()
      }
    }
  }

  cargarPedido(){
    this.pedidoService.getById(this.idPedido).subscribe({
      next: (v)=>{
        const data = v.data
        const pedido = data.pedido
        const detalle = data.detalle

        this.pedidoGroup.patchValue({
          id: pedido.id,
          idCliente: pedido.cliente.id,
          monedaEnum: pedido.monedaEnum,
          fechaEmision: this.pipe.transform(pedido.fechaEmision,'yyyy-MM-dd')
        })

        this.cliente = pedido.cliente
        this.clienteSelected = this.cliente
        this.detalle = detalle
        this.calcularTotal()
      },
      error: (e)=>{
        console.error(e)
        this.loading = false
        this.alertaService.alertError("Ups, ocurrió un error",
        ()=>{
          this.router.navigate(['/pedidos'], { replaceUrl: true });
        })
      }
    })
  }

  searchCliente: OperatorFunction<string, readonly IClienteSearchResponse[]> = (text$: Observable<string>) =>
  text$.pipe(
    debounceTime(300),
    distinctUntilChanged(),
    tap(() => this.searchingCliente = true),
    switchMap(term =>
      this.clienteService.search({
        nombre: term,
        page: 1,
        pageSize: 10
      })
      .pipe(
        map((v)=>v.data.data),
        tap(()=>this.searchClienteFailed = false),
        catchError(() => {
          this.searchClienteFailed = true;
          return of([]);
        }),

      )
    ),
    tap(() => this.searchingCliente = false),
  );

  onClienteChange(){
    if(this.cliente.id == undefined){
      return
    }

    this.clienteSelected = this.cliente
    this.pedidoGroup.patchValue({
      idCliente: this.cliente.id
    })
  }

  onClienteBlur(){
    if(this.cliente?.id == undefined){
      this.cliente = this.clienteSelected
    }
  }

  agregarProducto(){
    const modal = this.modalService.open(ProductoAddComponent,{ centered: true });
    modal.result.then((v) => {
      if(v != undefined){
        this.detalle.push(v)
        this.calcularTotal()
      }
    },(reason)=>{
      console.log("reason:" + reason)
    });
  }

  calcularTotal(){
    let total = 0
    for(let i = 0; i < this.detalle.length; i++){
      total += this.detalle[i].total
    }
    this.total = total
  }

  eliminarDetalle(index: number){
    this.detalle.splice(index, 1)
    this.calcularTotal()
  }

  guardar(){
    if(this.pedidoGroup.invalid){
      return;
    }

    this.loading = true;

    if(this.accion == AccionEnum.NUEVO){
      const _pedido: IPedidoPost = Object.assign({},this.pedidoGroup.value);
      const _detalle: IPedidoDetallePost[] = [];

      this.detalle.map((x)=>{
        const item: IPedidoDetallePost = {
          idProducto: x.idProducto,
          cantidad: x.cantidad,
          precioUnitario: x.precioUnitario
        };
        _detalle.push(item);
      });

      const model: IPedidoPostRequest = {
        pedido: _pedido,
        detalle: _detalle
      };

      this.pedidoService.post(model).subscribe({
        next: (v) =>{
          console.log(v)
          this.loading = false
          this.alertaService.alertOk("Se guardó con éxito")
        },
        error: (e) =>{
          console.error(e)
          this.loading = false
          this.alertaService.alertError("Ups, ocurrió un error")
        }
      });

      return;
    }

    if(this.accion == AccionEnum.EDITAR){
      const _pedido: IPedidoPut = Object.assign({},this.pedidoGroup.value);
      const _detalle: IPedidoDetallePut[] = [];

      this.detalle.map((x)=>{
        const item: IPedidoDetallePut = {
          idProducto: x.idProducto,
          cantidad: x.cantidad,
          precioUnitario: x.precioUnitario
        };
        _detalle.push(item);
      });

      const model: IPedidoPutRequest = {
        pedido: _pedido,
        detalle: _detalle
      };

      this.pedidoService.put(model).subscribe({
        next: (v) =>{
          console.log(v)
          this.loading = false
          this.alertaService.alertOk("Se guardó con éxito")
        },
        error: (e) =>{
          console.error(e)
          this.loading = false
          this.alertaService.alertError("Ups, ocurrió un error")
        }
      })
    }
  }

}

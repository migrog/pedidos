import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { IPedidoSearchRequest, IPedidoSearchResponse } from 'src/app/shared/modelos/interfaces/IPedido';
import { PedidoService } from 'src/app/shared/services/pedido.service';
import { AlertaService } from 'src/app/utils/alerta.service';

@Component({
  selector: 'app-pedido-list',
  templateUrl: './pedido-list.component.html',
  styleUrls: ['./pedido-list.component.css']
})
export class PedidoListComponent implements OnInit {

  //tools
  searchModel!: FormGroup;
  loading: boolean = false;

  //resultados
  pedidos: IPedidoSearchResponse[] = [];
  idPedido!: number;
  result: any;

  //paginacion
  selectedRow!: number;
  mostrados: number = 0;
  page: number = 1; // página actual
  pageSize: number = 10;//numeros de elementos por página
  selectedPage!: number;

  constructor(private router: Router, public alertaService: AlertaService, private fb: FormBuilder, private pedidoService: PedidoService) { }

  ngOnInit() {
    this.searchModel = this.fb.group({
      fechaEmision: [null],
      page: this.page,
      pageSize: this.pageSize
    });
    this.search()
  }

  //busqueda
  search() {
    let model: IPedidoSearchRequest;
    model = Object.assign({}, this.searchModel.value);
    this.pedidoService.search(model).subscribe({
      next: (v)=>{
        this.result = v.data
        this.pedidos = []
        this.pedidos = this.result.data.map((pedido: any, i: number) => ({ row: (this.page - 1) * this.pageSize + (i + 1), ...pedido }));

        const total = this.result.totalRows;
        this.mostrados = (this.pageSize * this.page < total ? this.pageSize : total - ((this.pageSize * this.page) - this.pageSize));

        this.loading = false
      },
      error: (e)=>{
        console.error(e)
        this.loading = false
      }
    })
  }

  paginar() {
    this.searchModel.patchValue({
      page: this.page,
      pageSize: this.pageSize
    })
    this.search()
  }

  //marcar fila
  setClickedRow(i: number) {
    this.selectedRow = i;
    this.selectedPage = this.page;
  }

  nuevo() {
    this.router.navigate(['/pedidos/new'], { replaceUrl: true });
  }
  eliminar(id: number){
    this.alertaService.alertConfirm("¿Esta seguro de eliminar el registro?","",
    ()=>{
      this.pedidoService.delete(id).subscribe({
        next: (v) =>{
          this.alertaService.alertOk(v.data)
          this.search()
        },
        error: (e)=>{
          console.error(e)
          this.loading = false
          this.alertaService.alertError("Ups, ocurrió un error")
        }
      })
    })
  }
}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { catchError, debounceTime, distinctUntilChanged, map, Observable, of, OperatorFunction, switchMap, tap } from 'rxjs';
import { IProductoAdd } from '../../modelos/interfaces/IPedido';
import { IProductoSearchResponse } from '../../modelos/interfaces/IProducto';
import { ProductoService } from '../../services/producto.service';

@Component({
  selector: 'app-producto-add',
  templateUrl: './producto-add.component.html',
  styleUrls: ['./producto-add.component.css']
})
export class ProductoAddComponent implements OnInit {
  //producto
  producto!: IProductoSearchResponse;
  formatter = (producto: IProductoSearchResponse) => producto.nombre
  searchingProducto = false;
  searchProductoFailed = false;

  productoAddGroup!: FormGroup;

  loading: boolean = false;

  constructor(private fb: FormBuilder,  public activeModal: NgbActiveModal, private productoService: ProductoService) { }

  ngOnInit(): void {
    this.productoAddGroup = this.fb.group(
      {
        idProducto: [''],
        nombreProducto: [''],
        cantidad: [1],
        precioUnitario: [0, [Validators.required, Validators.min(0)]],
        moneda:['']
      }
    );
  }

  close(){
    this.activeModal.close();
  }

  get f() { return this.productoAddGroup.controls; }

  searchProducto: OperatorFunction<string, readonly IProductoSearchResponse[]> = (text$: Observable<string>) =>
  text$.pipe(
    debounceTime(300),
    distinctUntilChanged(),
    tap(() => this.searchingProducto = true),
    switchMap(term =>
      this.productoService.search({
        nombre: term,
        page: 1,
        pageSize: 10
      })
      .pipe(
        map((v)=>v.data.data),
        tap(()=>{
          this.searchProductoFailed = false
        }),
        catchError(() => {
          this.searchProductoFailed = true;
          return of([]);
        }),

      )
    ),
    tap((v) =>{
       this.searchingProducto = false
      }),
  );

  onProductoChange(){
    if(this.producto.id == undefined){
      return
    }
    this.productoAddGroup.patchValue({
      idProducto: this.producto.id,
      nombreProducto: this.producto.nombre,
      moneda: this.producto.moneda,
      precioUnitario: this.producto.precioUnitario
    })
  }
  onProductoBlur(){
    if(this.producto?.id == undefined){
      const _producto: any = {
        id: this.f.idProducto.value,
        nombre: this.f.nombre.value,
        precioUnitario: this.f.precioUnitario.value,
        moneda: this.f.moneda.value
      }
      this.producto = _producto
    }
  }

  agregar(){
    let productoAdd: IProductoAdd
    productoAdd =  Object.assign({}, this.productoAddGroup.value);
    productoAdd.total = productoAdd.cantidad * productoAdd.precioUnitario
    this.activeModal.close(productoAdd)
  }
}

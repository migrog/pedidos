import { Component, Injectable, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IProductoSearchRequest, IProductoSearchResponse } from 'src/app/shared/modelos/interfaces/IProducto';
import { ProductoNewComponent } from '../producto-new/producto-new.component';
import { AccionEnum } from 'src/app/utils/enums';
import { ProductoService } from 'src/app/shared/services/producto.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AlertaService } from 'src/app/utils/alerta.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { catchError, debounceTime, distinctUntilChanged, map, switchMap, tap } from 'rxjs/operators';
import { Observable, of, OperatorFunction } from 'rxjs';

@Component({
  selector: 'app-producto-list',
  templateUrl: './producto-list.component.html',
  styleUrls: ['./producto-list.component.css']
})



export class ProductoListComponent implements OnInit {

  //tools
  searchModel!: FormGroup;
  loading: boolean = false;

  //resultados
  productos: IProductoSearchResponse[] = [];
  idProducto!: number;
  result: any;

  //paginacion
  selectedRow!: number;
  mostrados: number = 0;
  page: number = 1; // página actual
  pageSize: number = 10;//numeros de elementos por página
  selectedPage!: number;

  constructor(private spinner: NgxSpinnerService, private modalService: NgbModal, private productoService: ProductoService, public alertaService: AlertaService, private fb: FormBuilder) { }

  ngOnInit() {
    this.searchModel = this.fb.group({
      nombre: [''],
      page: this.page,
      pageSize: this.pageSize
    });
    this.search()
  }

  //busqueda
  search() {
    let model: IProductoSearchRequest;
    model = Object.assign({}, this.searchModel.value);
    this.productoService.search(model).subscribe({
      next: (v) =>{
        this.result = v.data;

        this.productos = [];
        this.productos = this.result.data.map((producto: any, i: number) => ({ row: (this.page - 1) * this.pageSize + (i + 1), ...producto }));

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
    console.log(this.page)
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
    const modal = this.modalService.open(ProductoNewComponent,{ centered: true });
    modal.componentInstance.accion = AccionEnum.NUEVO
    modal.result.then(() => {
      this.search()
    },(reason)=>{
      console.log(reason)
    });
  }
  editar(id: number) {
    const modal = this.modalService.open(ProductoNewComponent,{ centered: true });
    modal.componentInstance.accion = AccionEnum.EDITAR
    modal.componentInstance.idProducto = id
    modal.result.then(() => {
      this.search()
    },(reason)=>{
      console.log(reason)
    });
  }
  eliminar(id: number, i: number){
    this.alertaService.alertConfirm("¿Esta seguro de eliminar el registro?","",
    ()=>{
      this.productoService.delete(id).subscribe({
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

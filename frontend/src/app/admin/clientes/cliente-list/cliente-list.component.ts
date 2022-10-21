import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { IClienteSearchResponse, IClienteSearchRequest } from 'src/app/shared/modelos/interfaces/ICliente';
import { ClienteService } from 'src/app/shared/services/cliente.service';
import { AlertaService } from 'src/app/utils/alerta.service';
import { AccionEnum } from 'src/app/utils/enums';
import { ClienteNewComponent } from '../cliente-new/cliente-new.component';

@Component({
  selector: 'app-cliente-list',
  templateUrl: './cliente-list.component.html',
  styleUrls: ['./cliente-list.component.css']
})
export class ClienteListComponent implements OnInit {

  //tools
  searchModel!: FormGroup;
  loading: boolean = false;

  //resultados
  clientes: IClienteSearchResponse[] = [];
  idCliente!: number;
  result: any;

  //paginacion
  selectedRow!: number;
  mostrados: number = 0;
  page: number = 1; // página actual
  pageSize: number = 10;//numeros de elementos por página
  selectedPage!: number;

  constructor(private modalService: NgbModal, private clienteService: ClienteService, public alertaService: AlertaService, private fb: FormBuilder) { }

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
    let model: IClienteSearchRequest;
    model = Object.assign({}, this.searchModel.value);
    this.clienteService.search(model).subscribe({
      next: (v) =>{
        this.result = v.data;

        this.clientes = [];
        this.clientes = this.result.data.map((cliente: any, i: number) => ({ row: (this.page - 1) * this.pageSize + (i + 1), ...cliente }));

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
    let modal = this.modalService.open(ClienteNewComponent,{ centered: true });
    modal.componentInstance.accion = AccionEnum.NUEVO
    modal.result.then((v) => {
      this.search()
    },(reason)=>{
      console.log(reason)
    });
  }
  editar(id: number) {
    const modal = this.modalService.open(ClienteNewComponent,{ centered: true });
    modal.componentInstance.accion = AccionEnum.EDITAR
    modal.componentInstance.idCliente = id
    modal.result.then((v) => {
      this.search()
    },(reason)=>{
      console.log(reason)
    });
  }
  eliminar(id: number){
    this.alertaService.alertConfirm("¿Esta seguro de eliminar el registro?","",
    ()=>{
      this.clienteService.delete(id).subscribe({
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

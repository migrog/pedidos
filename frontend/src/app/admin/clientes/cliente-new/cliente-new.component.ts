import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IClientePostRequest, IClientePutRequest } from 'src/app/shared/modelos/interfaces/ICliente';
import { ClienteService } from 'src/app/shared/services/cliente.service';
import { AlertaService } from 'src/app/utils/alerta.service';
import { AccionEnum } from 'src/app/utils/enums';

@Component({
  selector: 'app-cliente-new',
  templateUrl: './cliente-new.component.html',
  styleUrls: ['./cliente-new.component.css']
})
export class ClienteNewComponent implements OnInit {
  title: string = "Nuevo cliente"
  idCliente: number = 0;
  accion: AccionEnum = AccionEnum.NUEVO;
  loading: boolean = false;
  clienteGroup!: FormGroup;

  constructor(private fb: FormBuilder, public activeModal: NgbActiveModal, public alertaService: AlertaService, private clienteService: ClienteService) { }

  ngOnInit(): void {
    this.clienteGroup = this.fb.group(
      {
        id: [''],
        nombre: ['', Validators.required],
      }
    );
    if(this.accion == AccionEnum.EDITAR){
      this.title = "Editar cliente"
      this.cargarCliente(this.idCliente)
    }
  }

  get f() { return this.clienteGroup.controls; }

  close(){
    this.activeModal.dismiss("click cerrar");
  }

  guardar(){
    if(this.clienteGroup.invalid){
      return;
    }

    this.loading = true;

    if(this.accion == AccionEnum.NUEVO){
      let model: IClientePostRequest;
      model = Object.assign({}, this.clienteGroup.value);
      this.clienteService.post(model).subscribe({
        next: (v) =>{
          console.log(v)
          this.loading = false
          this.alertaService.alertOk("Se guardó con éxito","",
          ()=>{
            this.activeModal.close("click guardar");
          })
        },
        error: (e) =>{
          console.error(e)
          this.loading = false
          this.alertaService.alertError("Ups, ocurrió un error")
        }
      })
      return
    }

    if(this.accion == AccionEnum.EDITAR){
      let model: IClientePutRequest;
      model = Object.assign({}, this.clienteGroup.value);
      this.clienteService.put(model).subscribe({
        next: (v) =>{
          this.loading = false
          this.alertaService.alertOk("Se guardó con éxito","",
          ()=>{
            this.activeModal.close("click guardar")
          })
        },
        error: (e)=>{
          console.error(e)
          this.loading = false
          this.alertaService.alertError("Ups, ocurrió un error")
        }
      })
    }

  }
  cargarCliente(id: number){
    this.clienteService.getById(id).subscribe({
      next: (v) =>{
        const data = v.data;
        this.clienteGroup.patchValue({
          id: data.id,
          nombre: data.nombre,
          precioUnitario: data.precioUnitario,
          monedaEnum: data.monedaEnum
        })
      },
      error: (e)=>{
        console.error(e)
        this.loading = false
        this.alertaService.alertError("Ups, ocurrió un error")
      }
    })
  }
}

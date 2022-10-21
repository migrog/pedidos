import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertaService } from 'src/app/utils/alerta.service';
import { IProductoPostRequest, IProductoPutRequest } from 'src/app/shared/modelos/interfaces/IProducto';
import { ProductoService } from 'src/app/shared/services/producto.service';
import { AccionEnum } from 'src/app/utils/enums';

@Component({
  selector: 'app-producto-new',
  templateUrl: './producto-new.component.html',
  styleUrls: ['./producto-new.component.css']
})
export class ProductoNewComponent implements OnInit {
  title: string = "Nuevo producto"
  idProducto: number = 0;
  accion: AccionEnum = AccionEnum.NUEVO;
  loading: boolean = false;
  productoGroup!: FormGroup;

  constructor(private fb: FormBuilder, public activeModal: NgbActiveModal,public alertaService: AlertaService, private productoService: ProductoService) { }

  ngOnInit(): void {
    this.productoGroup = this.fb.group(
      {
        id: [''],
        nombre: ['', Validators.required],
        precioUnitario: ['', [Validators.required, Validators.min(0)]],
        monedaEnum: ['1', Validators.required],
      }
    );
    if(this.accion == AccionEnum.EDITAR){
      this.title = "Editar producto"
      this.cargarProducto(this.idProducto)
    }
  }

  get f() { return this.productoGroup.controls; }

  close(){
    this.activeModal.dismiss("click cerrar");
  }

  guardar(){
    if(this.productoGroup.invalid){
      return;
    }

    this.loading = true;

    if(this.accion == AccionEnum.NUEVO){
      let model: IProductoPostRequest;
      model = Object.assign({}, this.productoGroup.value);
      this.productoService.post(model).subscribe({
        next: (v) =>{
          console.log(v)
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
      return
    }

    if(this.accion == AccionEnum.EDITAR){
      let model: IProductoPutRequest;
      model = Object.assign({}, this.productoGroup.value);
      this.productoService.put(model).subscribe({
        next: (v) =>{
          console.log(v)
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
  cargarProducto(id: number){
    this.productoService.getById(id).subscribe({
      next: (v) =>{
        const data = v.data;
        this.productoGroup.patchValue({
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

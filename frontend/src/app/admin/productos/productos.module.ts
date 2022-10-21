import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule, NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { ProductosRoutingModule } from './productos-routing.module';
import { ProductoListComponent } from './producto-list/producto-list.component';
import { ProductoNewComponent } from './producto-new/producto-new.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';


@NgModule({
  declarations: [
    ProductoListComponent,
    ProductoNewComponent
  ],
  imports: [
    CommonModule,
    ProductosRoutingModule,
    NgbPaginationModule,
    NgbModule,
    ReactiveFormsModule,
    FormsModule,
    NgxSpinnerModule
  ]
})
export class ProductosModule { }

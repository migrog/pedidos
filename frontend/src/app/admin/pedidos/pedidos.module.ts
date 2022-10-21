import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { PedidosRoutingModule } from './pedidos-routing.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgbPaginationModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerModule } from 'ngx-spinner';
import { PedidoListComponent } from './pedido-list/pedido-list.component';
import { PedidoNewComponent } from './pedido-new/pedido-new.component';
import { ProductoAddComponent } from 'src/app/shared/componentes/producto-add/producto-add.component';


@NgModule({
  declarations: [
    PedidoListComponent,
    PedidoNewComponent,
    ProductoAddComponent
  ],
  imports: [
    CommonModule,
    PedidosRoutingModule,
    NgbPaginationModule,
    NgbModule,
    ReactiveFormsModule,
    FormsModule,
    NgxSpinnerModule
  ],
  providers: [DatePipe],
  entryComponents: [ProductoAddComponent]
})
export class PedidosModule { }

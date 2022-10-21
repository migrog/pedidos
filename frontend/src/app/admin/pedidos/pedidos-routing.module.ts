import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PedidoListComponent } from './pedido-list/pedido-list.component';
import { PedidoNewComponent } from './pedido-new/pedido-new.component';

const routes: Routes = [
  { path: '', component: PedidoListComponent },
  { path: 'new', component: PedidoNewComponent },
  { path: 'edit/:id', component: PedidoNewComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PedidosRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';

const routes: Routes = [
  {
    path: '',
    component: AdminComponent,
    children: [
      { path: '', redirectTo: 'pedidos', pathMatch: 'prefix' },
      { path: 'pedidos', loadChildren: ()=> import('./pedidos/pedidos.module').then(m=>m.PedidosModule) },
      { path: 'productos', loadChildren: ()=> import('./productos/productos.module').then(m=>m.ProductosModule) },
      { path: 'clientes', loadChildren: ()=> import('./clientes/clientes.module').then(m=>m.ClientesModule) }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }

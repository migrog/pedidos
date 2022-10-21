import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClientesRoutingModule } from './clientes-routing.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgbPaginationModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ClienteListComponent } from './cliente-list/cliente-list.component';
import { ClienteNewComponent } from './cliente-new/cliente-new.component';


@NgModule({
  declarations: [
    ClienteListComponent,
    ClienteNewComponent
  ],
  imports: [
    CommonModule,
    ClientesRoutingModule,
    NgbPaginationModule,
    NgbModule,
    ReactiveFormsModule,
    FormsModule,
    NgxSpinnerModule
  ]
})
export class ClientesModule { }

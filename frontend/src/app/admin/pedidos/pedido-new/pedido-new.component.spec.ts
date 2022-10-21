import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PedidoNewComponent } from './pedido-new.component';

describe('PedidoNewComponent', () => {
  let component: PedidoNewComponent;
  let fixture: ComponentFixture<PedidoNewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PedidoNewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PedidoNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

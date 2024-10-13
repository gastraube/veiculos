import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VeiculosEditarComponent } from './veiculos.editar.component';

describe('VeiculosEditarComponent', () => {
  let component: VeiculosEditarComponent;
  let fixture: ComponentFixture<VeiculosEditarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VeiculosEditarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VeiculosEditarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

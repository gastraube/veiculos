import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VeiculosCriarComponent } from './veiculos.criar.component';

describe('VeiculosCriarComponent', () => {
  let component: VeiculosCriarComponent;
  let fixture: ComponentFixture<VeiculosCriarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VeiculosCriarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VeiculosCriarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

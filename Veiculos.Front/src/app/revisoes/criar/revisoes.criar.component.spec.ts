import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RevisoesCriarComponent } from './revisoes.criar.component';

describe('RevisoesCriarComponent', () => {
  let component: RevisoesCriarComponent;
  let fixture: ComponentFixture<RevisoesCriarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RevisoesCriarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RevisoesCriarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

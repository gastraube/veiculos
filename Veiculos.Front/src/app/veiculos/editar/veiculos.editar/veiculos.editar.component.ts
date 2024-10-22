import { Component, ElementRef, inject, Inject, ViewChild, viewChild } from '@angular/core';
import { Caminhao, Carro, Veiculo } from '../../veiculos.model';
import { VeiculosService } from '../../veiculos.service';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormBuilder } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { VeiculosComponent } from '../../veiculos.component'

@Component({
  selector: 'app-veiculos.editar',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './veiculos.editar.component.html',
  styleUrl: './veiculos.editar.component.css'
})
export class VeiculosEditarComponent {
activeModal = inject(NgbActiveModal);
id!: number;
veiculo: Veiculo = new Veiculo;
myForm!: FormGroup;
tipoVeiculos = [{id: 1, nome: "Carro"}, {id: 2, nome: "Caminhao"}];


constructor(private _veiculosService:VeiculosService, private fb: FormBuilder) {    

}

ngOnInit() {

  // this.veiculo.carro = new Carro();
  // this.veiculo.caminhao = new Caminhao();

  this._veiculosService.GetVeiculo(this.id).subscribe((res: any) => {
    debugger;
    this.myForm = this.fb.group({
      tipoVeiculo: [res.tipoVeiculo],
      id: res.id,
      ano: res.ano,
      modelo: res.modelo,
      placa: res.placa,
      cor: res.cor,
      capacidadeCarga: res.capacidadeCarga,
      capacidadePassageiro: res.capacidadePassageiro
    });
  });
};

  onSubmit() {
    debugger
    
    var form = this.myForm.value;
    this.veiculo.id = form.id;
    this.veiculo.ano = form.ano;
    this.veiculo.placa = form.placa;
    this.veiculo.modelo = form.modelo;
    this.veiculo.cor = form.cor;
    this.veiculo.capacidadePassageiro = form.capacidadePassageiro;
    this.veiculo.capacidadeCarga = form.capacidadeCarga;
    this.veiculo.tipoVeiculo= form.tipoVeiculo;

    this._veiculosService.UpdateVeiculo(this.veiculo).subscribe((res: any) => {
      this.activeModal.close('Close click')
      window.location.reload();
		});

   
  }
}

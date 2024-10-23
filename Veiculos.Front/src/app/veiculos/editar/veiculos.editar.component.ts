import { Component, inject } from '@angular/core';
import { Veiculo } from '../veiculos.model';
import { VeiculosService } from '../veiculos.service';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormBuilder } from '@angular/forms';
import { RevisoesCriarComponent } from '../../revisoes/criar/revisoes.criar.component'
import { NgbActiveModal, NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { Revisao } from '../../revisoes/revisoes.model';
import { RevisoesService } from '../../revisoes/revisoes.service';

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
result!: string;
revisoes!: Array<Revisao>;
veiculoId!:number;

private modalService = inject(NgbModal);

constructor(private _veiculosService:VeiculosService, 
            private fb: FormBuilder,
            private _revisaoService:RevisoesService) {    }

ngOnInit() {
  this.revisoes = new Array<Revisao>();

  this._revisaoService.GetRevisoesByVeiculoId(this.id).subscribe((res: any) => {   
    res.forEach((r: Revisao) => {
      r.tempId = Math.random()
      this.revisoes.push(r);
      debugger;
    });
  });    
  


  this._veiculosService.GetVeiculo(this.id).subscribe((res: any) => {
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
        this._revisaoService.CreateRevisoes(this.revisoes, this.veiculo.id).subscribe((res: any) => {
          console.log("revisao criada")
      });

      debugger
      this.activeModal.close('Close click')
      window.location.reload();
		});    
  }

  openCriarRevisoes(veiculoId: number) {
    let options:NgbModalOptions = {
      size: 'md',
      backdrop: 'static'
    };

    const activeModal = this.modalService.open(RevisoesCriarComponent, options);
    activeModal.componentInstance.veiculoId = veiculoId;
    activeModal.result.then(r => {
      if(r != undefined )
      {
        this.result = r
        this.revisoes.push(r)
      }
    });
  }

  removeRevisao(tempId: number){
    debugger;
    this.revisoes = this.revisoes.filter(r => r.tempId !== tempId)
  }

}

import { Component, inject } from '@angular/core';
import { NgbActiveModal, NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { Caminhao, Carro, Veiculo } from '../veiculos.model';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { VeiculosService } from '../veiculos.service';
import { CommonModule } from '@angular/common';
import { Revisao } from '../../revisoes/revisoes.model';
import { RevisoesService } from '../../revisoes/revisoes.service';
import { RevisoesCriarComponent } from '../../revisoes/criar/revisoes.criar.component';

@Component({
  selector: 'app-veiculos.criar',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './veiculos.criar.component.html',
  styleUrl: './veiculos.criar.component.css'
})
export class VeiculosCriarComponent {
  activeModal = inject(NgbActiveModal);
  id!: number;
  veiculo: Veiculo = new Veiculo;
  myForm!: FormGroup;
  tipoVeiculos = [{id: 1, nome: "Carro"}, {id: 2, nome: "Caminhao"}];
  revisoes!: Array<Revisao>;
  result!: string;

  constructor(private _veiculosService:VeiculosService, 
    private fb: FormBuilder,
    private _revisaoService:RevisoesService,
    private _modalService: NgbModal) {    
  
  }
  
  ngOnInit() {
    this.revisoes = new Array<Revisao>();

    this.myForm = this.fb.group({
      tipoVeiculo: "0",
      id: 0,
      ano: 0,
      modelo: "",
      placa: "",
      cor: "",
      capacidadeCarga: 0,
      capacidadePassageiro: 0
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
  
      this._veiculosService.CreateVeiculo(this.veiculo).subscribe((res: any) => {
        debugger
        this.revisoes.forEach(r => {
          r.veiculoId = res.id
        });

        this._revisaoService.CreateRevisoes(this.revisoes, res.id).subscribe((res: any) => {
          console.log("revisao criada")
        });

        this.activeModal.close('Close click')
        window.location.reload();
      });
    }

    openCriarRevisoes(veiculoId: number) {
      let options:NgbModalOptions = {
        size: 'md',
        backdrop: 'static'
      };
  
      const activeModal = this._modalService.open(RevisoesCriarComponent, options);
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

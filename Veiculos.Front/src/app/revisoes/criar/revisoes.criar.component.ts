import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import {  NgbActiveModal, NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { Subject } from 'rxjs';
import { Revisao } from '../revisoes.model';
import { RevisoesService } from '../revisoes.service';

@Component({
  selector: 'app-revisoes.criar',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './revisoes.criar.component.html',
  styleUrl: './revisoes.criar.component.css'
})
export class RevisoesCriarComponent {
  @Input() public veiculoId!: number;

  myForm!: FormGroup;
  revisao: Revisao = new Revisao;
  
  constructor(public activeModal: NgbActiveModal, private _revisaoService:RevisoesService, private fb: FormBuilder) {
   
  }

  ngOnInit() {
    this.myForm = this.fb.group({
      km: 0,
      data: new Date(),
      valorDaRevisao: 0 ,
      veiculoId: this.veiculoId,
      tempId:  Math.random()
    });
};

  onSubmit() {
    debugger;
    var form = this.myForm.value;
    this.revisao.tempId = form.tempId;
    this.revisao.km = form.km;
    this.revisao.data = form.data;
    this.revisao.valorDaRevisao = form.valorDaRevisao;
    this.revisao.veiculoId = form.veiculoId;

    this.activeModal.close(this.revisao);
  }

  onClose(){
    this.activeModal.close();
  }
  
}

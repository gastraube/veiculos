import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { VeiculosComponent } from '../app/veiculos/veiculos.component'
import { FormBuilder, FormGroup } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { CommonModule } from '@angular/common';
import { Directive, EventEmitter, Input, Output, QueryList, ViewChildren } from '@angular/core';
import { DecimalPipe } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, VeiculosComponent, FormsModule, ReactiveFormsModule, NgxPaginationModule,
     CommonModule, DecimalPipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Veiculos.Front';

 
}





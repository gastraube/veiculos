import { Component, Directive, EventEmitter, inject, Input, Output, QueryList, ViewChildren } from '@angular/core';
import { Veiculo } from './veiculos.model';
import { NgFor } from '@angular/common';
import { VeiculosService } from './veiculos.service';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { VeiculosEditarComponent } from './editar/veiculos.editar/veiculos.editar.component';
import { SearchPipe } from '../search.pipe';
import { FormsModule } from '@angular/forms';
import { VeiculosCriarComponent } from './criar/veiculos.criar/veiculos.criar.component';
import { NgxPaginationModule } from 'ngx-pagination';

@Directive({
	selector: 'th[sortable]',
	standalone: true,
	host: {
		'[class.asc]': 'direction === "asc"',
		'[class.desc]': 'direction === "desc"',
		'(click)': 'rotate()',
	},
})
export class NgbdSortableHeader {
	@Input() sortable: SortColumn = '';
	@Input() direction: SortDirection = '';
	@Output() sort = new EventEmitter<SortEvent>();

	rotate() {
		this.direction = rotate[this.direction];
		this.sort.emit({ column: this.sortable, direction: this.direction });
	}
}

@Component({
	selector: 'app-veiculos',
	standalone: true,
	imports: [NgFor, SearchPipe, FormsModule, NgxPaginationModule, NgbdSortableHeader],
	templateUrl: './veiculos.component.html',
	styleUrl: './veiculos.component.css'
})
export class VeiculosComponent {
	searchText = "";
	veiculos: Veiculo[] = [];
	private modalService = inject(NgbModal);
	p: number = 1;

	onSort({ column, direction }: SortEvent) {
		debugger;

		for (const header of this.headers) {
			if (header.sortable !== column) {
				header.direction = '';
			}
		}

		if (direction === '' || column === '') {
			this.veiculos = this.veiculos;
		} else {
			this.veiculos = [...this.veiculos].sort((a, b) => {
				const res = compare(a[column], b[column]);
				return direction === 'asc' ? res : -res;
			});
		}
	}

	openEditar(id: number) {
		const activeModal = this.modalService.open(VeiculosEditarComponent, { size: 'lg' });
		activeModal.componentInstance.id = id;
	}

	openCriar() {
		const activeModal = this.modalService.open(VeiculosCriarComponent, { size: 'lg' });

	}

	deletar(id: number) {
		debugger;

		this._veiculosService.DeleteVeiculo(id).subscribe((res: any) => {
			window.location.reload();
		});

	}

	constructor(private _veiculosService: VeiculosService) { }

	ngOnInit() {
		this._veiculosService.GetVeiculos().subscribe((res: any) => {
			console.log(res)
			this.veiculos = res
		});

	};

	@ViewChildren(NgbdSortableHeader) headers!: QueryList<NgbdSortableHeader>;

}


export type SortColumn = keyof Veiculo | '';
export type SortDirection = 'asc' | 'desc' | '';
const rotate: { [key: string]: SortDirection } = { asc: 'desc', desc: '', '': 'asc' };

const compare = (v1: string | number, v2: string | number) => (v1 < v2 ? -1 : v1 > v2 ? 1 : 0);

export interface SortEvent {
	column: SortColumn;
	direction: SortDirection;
}
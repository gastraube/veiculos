import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';
import { Revisao } from './revisoes.model';

@Injectable({
  providedIn: 'root'
})
export class RevisoesService {

  constructor(private httpClient: HttpClient) { }

  GetRevisoesByVeiculoId(veiculoId: number) : Observable<any>{
    return this.httpClient.get<any[]>(`${environment.apiUrl}/api/Revisao?VeiculoId=${veiculoId}`);
  }

  CreateRevisoes(revisoes: Array<Revisao>, veiculoId: number) : Observable<any>{
    debugger
    return this.httpClient.post(`${environment.apiUrl}/api/Revisao?veiculoId=${veiculoId}`, revisoes);
  }
}

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Veiculo } from './veiculos.model';
import { environment } from './../../environments/environment.development';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VeiculosService {

  constructor(private httpClient: HttpClient) { }

  GetVeiculos() : Observable<any>{
      return this.httpClient.get<any[]>(`${environment.apiUrl}/api/Veiculo`);
  }

  GetVeiculo(id: number) : Observable<any>{
    return this.httpClient.get<any[]>(`${environment.apiUrl}/id?Id=${id}`);
  }

  UpdateVeiculo(veiculo: Veiculo) : Observable<any>{
    return this.httpClient.put(`${environment.apiUrl}/api/Veiculo`, veiculo);
  }

  CreateVeiculo(veiculo: Veiculo) : Observable<any>{
    return this.httpClient.post(`${environment.apiUrl}/api/Veiculo`, veiculo);
  }

  DeleteVeiculo(id: number) : Observable<any>{
    return this.httpClient.delete(`${environment.apiUrl}/api/Veiculo/${id}`);
  }
}


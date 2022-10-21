import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IClienteSearchRequest, IClientePostRequest, IClientePutRequest } from '../modelos/interfaces/ICliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  controlador: string = '';
  private headerOptions: any = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private httpClient: HttpClient) {
    this.controlador = environment.apiUrl + environment.endPoint.cliente;
  }

  public search(model: IClienteSearchRequest): Observable<any> {
    const url = this.controlador + "/search";
    return this.httpClient.post(url, model, this.headerOptions);
  }
  public post(model: IClientePostRequest): Observable<any>{
    const url = this.controlador;
    return this.httpClient.post(url, model, this.headerOptions);
  }
  public getById(id: number): Observable<any>{
    const url = this.controlador + `/${id}`;
    return this.httpClient.get(url, this.headerOptions);
  }
  public put(model: IClientePutRequest): Observable<any>{
    const url = this.controlador;
    return this.httpClient.put(url, model, this.headerOptions);
  }
  public delete(id: number): Observable<any>{
    const url = this.controlador + `/${id}`;
    return this.httpClient.delete(url, this.headerOptions);
  }
}

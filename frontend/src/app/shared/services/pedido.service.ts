import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IPedidoPostRequest, IPedidoPutRequest, IPedidoSearchRequest } from '../modelos/interfaces/IPedido';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {
  controlador: string = '';
  private headerOptions: any = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private httpClient: HttpClient) {
    this.controlador = environment.apiUrl + environment.endPoint.pedido;
  }

  public search(model: IPedidoSearchRequest): Observable<any> {
    const url = this.controlador + "/search";
    return this.httpClient.post(url, model, this.headerOptions);
  }

  public post(model: IPedidoPostRequest): Observable<any>{
    const url = this.controlador;
    return this.httpClient.post(url, model, this.headerOptions);
  }

  public getById(id: number): Observable<any>{
    const url = this.controlador + `/${id}`;
    return this.httpClient.get(url, this.headerOptions);
  }

  public put(model: IPedidoPutRequest): Observable<any>{
    const url = this.controlador;
    return this.httpClient.put(url, model, this.headerOptions);
  }
  public delete(id: number): Observable<any>{
    const url = this.controlador + `/${id}`;
    return this.httpClient.delete(url, this.headerOptions);
  }
}

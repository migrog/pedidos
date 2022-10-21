import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IProductoPostRequest, IProductoPutRequest, IProductoSearchRequest } from '../modelos/interfaces/IProducto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {

  controlador: string = '';
  private headerOptions: any = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private httpClient: HttpClient) {
    this.controlador = environment.apiUrl + environment.endPoint.producto;
  }

  public search(model: IProductoSearchRequest): Observable<any> {
    const url = this.controlador + "/search";
    return this.httpClient.post(url, model, this.headerOptions);
  }
  public post(model: IProductoPostRequest): Observable<any>{
    const url = this.controlador;
    return this.httpClient.post(url, model, this.headerOptions);
  }
  public getById(id: number): Observable<any>{
    const url = this.controlador + `/${id}`;
    return this.httpClient.get(url, this.headerOptions);
  }
  public put(model: IProductoPutRequest): Observable<any>{
    const url = this.controlador;
    return this.httpClient.put(url, model, this.headerOptions);
  }
  public delete(id: number): Observable<any>{
    const url = this.controlador + `/${id}`;
    return this.httpClient.delete(url, this.headerOptions);
  }
}

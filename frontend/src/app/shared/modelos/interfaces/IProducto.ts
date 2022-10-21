export interface IProductoSearchRequest{
  nombre:string;
  page: number;
  pageSize: number;
}

export interface IProductoSearchResponse{
  row: number;
  id: number;
  nombre: string;
  precioUnitario: number;
  moneda: string;
}

export interface IProductoPostRequest{
  nombre: string;
  precioUnitario: number;
  monedaEnum: string;
}

export interface IProductoPutRequest{
  id: number;
  nombre: string;
  precioUnitario: number;
  monedaEnum: string;
}

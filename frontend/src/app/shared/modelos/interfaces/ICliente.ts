export interface IClienteSearchRequest{
  nombre:string;
  page: number;
  pageSize: number;
}

export interface IClienteSearchResponse{
  row: number;
  id: number;
  nombre: string;
}

export interface IClientePostRequest{
  nombre: string;
}

export interface IClientePutRequest{
  id: number;
  nombre: string;
}

//post
export interface IPedidoPostRequest{
  pedido: IPedidoPost
  detalle: IPedidoDetallePost[]
}

export interface IPedidoPost{
  idCliente: number
  monedaEnum: string
  fechaEmision: Date
}

export interface IPedidoDetallePost{
  idProducto: number
  cantidad: number
  precioUnitario: number
}

//put
export interface IPedidoPutRequest{
  pedido: IPedidoPut
  detalle: IPedidoDetallePut[]
}

export interface IPedidoPut{
  id: number
  idCliente: number
  monedaEnum: string
  fechaEmision: Date
}

export interface IPedidoDetallePut{
  idProducto: number
  cantidad: number
  precioUnitario: number
}

//get
export interface IProductoAdd{
  idProducto: number
  nombreProducto: string
  cantidad: number
  precioUnitario: number
  total:number
}

//search
export interface IPedidoSearchRequest{
  fechaEmision?:Date
  page: number
  pageSize: number
}

export interface IPedidoSearchResponse{
  row: number
  id: number
  cliente: string
  total: number
  moneda: string
  fechaEmision: Date
}

using System;
using System.Collections.Generic;

namespace pedidos.dominio.entidades.dto
{
    public class PedidoPostRequest
    {
        public PedidoPost Pedido { get; set; }
        public List<PedidoDetallePost> Detalle { get; set; }
    }
    public class PedidoPost
    {
        public int IdCliente { get; set; }
        public string MonedaEnum { get; set; }
        public DateTime FechaEmision { get; set; }
    }
    public class PedidoDetallePost
    {
        public int IdProducto { get; set; }
        public double Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
    }
}

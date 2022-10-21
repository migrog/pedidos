using System;
using System.Collections.Generic;
using System.Text;

namespace pedidos.dominio.entidades.dto
{
    public class PedidoGetResponse
    {
        public PedidoGet Pedido { get; set; }
        public List<PedidoDetalleGet> Detalle { get; set; }
    }
    public class PedidoGet
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public string MonedaEnum { get; set; }
        public DateTime FechaEmision { get; set; }

    }
    public class PedidoDetalleGet {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public double Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double Total { get; set; }

    }
}

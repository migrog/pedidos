using System;
using System.Collections.Generic;
using System.Text;

namespace pedidos.dominio.entidades.dto
{
    public class PedidoPutRequest
    {
        public PedidoPut Pedido { get; set; }
        public List<PedidoDetallePut> Detalle { get; set; }
    }
    public class PedidoPut
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string MonedaEnum { get; set; }
        public DateTime FechaEmision { get; set; }
    }
    public class PedidoDetallePut
    {
        public int IdProducto { get; set; }
        public double Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
    }
}

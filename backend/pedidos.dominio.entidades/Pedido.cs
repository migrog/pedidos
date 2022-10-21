using System;

namespace pedidos.dominio.entidades
{
    public class Pedido: EntidadBase
    {
        public int IdCliente { get; set; }
        public double Total { get; set; }
        public string MonedaEnum { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaRegistro { get; set; }

    }
}

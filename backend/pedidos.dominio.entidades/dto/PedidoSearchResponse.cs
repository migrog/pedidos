using System;
using System.Collections.Generic;
using System.Text;

namespace pedidos.dominio.entidades.dto
{
    public class PedidoSearchResponse
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public double Total { get; set; }
        public string Moneda { get; set; }
        public DateTime FechaEmision { get; set; }

    }
}

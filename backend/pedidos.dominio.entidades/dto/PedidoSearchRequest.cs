using System;
using System.Collections.Generic;
using System.Text;

namespace pedidos.dominio.entidades.dto
{
    public class PedidoSearchRequest
    {
        public string FechaEmision { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

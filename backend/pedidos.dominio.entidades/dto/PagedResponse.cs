using System.Collections.Generic;

namespace pedidos.dominio.entidades.dto
{
    public class PagedResponse<T> where T : class
    {
        public List<T> Data { get; set; }
        public int TotalRows { get; set; }
    }
}

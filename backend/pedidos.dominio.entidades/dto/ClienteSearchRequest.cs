﻿namespace pedidos.dominio.entidades.dto
{
    public class ClienteSearchRequest
    {
        public string Nombre { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

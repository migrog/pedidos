using System;
using System.Collections.Generic;
using System.Text;
using pedidos.dominio.entidades.dto;

namespace pedidos.dominio.entidades.interfaces.repositorio
{
    public interface IClienteRepositorio
    {
        PagedResponse<ClienteSearchResponse> Search(ClienteSearchRequest model);
    }
}

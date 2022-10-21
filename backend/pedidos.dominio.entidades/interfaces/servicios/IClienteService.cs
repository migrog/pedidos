using pedidos.dominio.entidades.dto;

namespace pedidos.dominio.entidades.interfaces.servicios
{
    public interface IClienteService: IServicioBase<Cliente>
    {
        PagedResponse<ClienteSearchResponse> Search(ClienteSearchRequest model);
    }
}

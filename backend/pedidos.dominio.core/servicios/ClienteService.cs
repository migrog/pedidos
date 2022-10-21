using pedidos.dominio.entidades;
using pedidos.dominio.entidades.dto;
using pedidos.dominio.entidades.interfaces.servicios;
using pedidos.infra.data.repositorio;

namespace pedidos.dominio.core.servicios
{
    public class ClienteService : ServicioBase<Cliente>, IClienteService
    {
        private ClienteRepositorio repo = new ClienteRepositorio();
        public PagedResponse<ClienteSearchResponse> Search(ClienteSearchRequest model)
        {
            return repo.Search(model);
        }
    }
}

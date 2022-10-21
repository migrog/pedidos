using System.Collections.Generic;
using pedidos.dominio.entidades.dto;

namespace pedidos.dominio.entidades.interfaces.repositorio
{
    public interface IPedidoRepositorio
    {
        PagedResponse<PedidoSearchResponse> Search(PedidoSearchRequest model);
        PedidoGetResponse GetById(int id);
        void Insert(Pedido pedido, List<PedidoDetalle> detalle);
        void Update(Pedido pedido, List<PedidoDetalle> detalle);

    }
}

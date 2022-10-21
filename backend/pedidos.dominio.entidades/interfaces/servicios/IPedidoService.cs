using System.Collections.Generic;
using pedidos.dominio.entidades.dto;

namespace pedidos.dominio.entidades.interfaces.servicios
{
    public interface IPedidoService: IServicioBase<Pedido>
    {
        PedidoGetResponse GetById(int id);
        PagedResponse<PedidoSearchResponse> Search(PedidoSearchRequest model);
        void Post(Pedido pedido, List<PedidoDetalle> detalle);
        void Put(Pedido pedido, List<PedidoDetalle> detalle);
    }
}

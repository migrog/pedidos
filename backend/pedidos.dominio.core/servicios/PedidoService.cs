using System;
using System.Collections.Generic;
using System.Text;
using pedidos.dominio.core.servicios;
using pedidos.dominio.entidades;
using pedidos.dominio.entidades.dto;
using pedidos.dominio.entidades.interfaces.servicios;
using pedidos.infra.data.repositorio;

namespace pedidos.dominio.core.servicios
{
    public class PedidoService : ServicioBase<Pedido>, IPedidoService
    {
        private PedidoRepositorio repo = new PedidoRepositorio();

        public PagedResponse<PedidoSearchResponse> Search(PedidoSearchRequest model)
        {
            return repo.Search(model);
        }
        public PedidoGetResponse GetById(int id)
        {
            return repo.GetById(id);
        }
        public void Post(Pedido pedido, List<PedidoDetalle> detalle)
        {
            repo.Insert(pedido, detalle);
        }

        public void Put(Pedido pedido, List<PedidoDetalle> detalle)
        {
            repo.Update(pedido, detalle);
        }
    }
}

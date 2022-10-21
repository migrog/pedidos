using System;
using System.Collections.Generic;
using System.Text;
using pedidos.dominio.entidades;
using pedidos.dominio.entidades.dto;
using pedidos.dominio.entidades.interfaces.servicios;
using pedidos.infra.data.repositorio;

namespace pedidos.dominio.core.servicios
{
    public class ProductoService:ServicioBase<Producto>, IProductoService
    {
        private ProductoRepositorio repo = new ProductoRepositorio();
        public PagedResponse<ProductoSearchResponse> Search(ProductoSearchRequest model)
        {
            return repo.Search(model);
        }
    }
}

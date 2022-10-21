using pedidos.dominio.entidades.dto;

namespace pedidos.dominio.entidades.interfaces.repositorio
{
    public interface IProductoRepositorio
    {
        PagedResponse<ProductoSearchResponse> Search(ProductoSearchRequest model);
    }
}

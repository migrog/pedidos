using pedidos.dominio.entidades.dto;

namespace pedidos.dominio.entidades.interfaces.servicios
{
    public interface IProductoService: IServicioBase<Producto>
    {
        PagedResponse<ProductoSearchResponse> Search(ProductoSearchRequest model);
    }
}

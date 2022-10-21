using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using pedidos.dominio.entidades;
using pedidos.dominio.entidades.dto;
using pedidos.dominio.entidades.interfaces.repositorio;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace pedidos.infra.data.repositorio
{
    public class ProductoRepositorio: RepositorioBase<Producto>, IProductoRepositorio
    {
        public PagedResponse<ProductoSearchResponse> Search(ProductoSearchRequest model)
        {
            var db = context.Database;
            using var cnx = db.GetDbConnection();

            var productos = new List<ProductoSearchResponse>();
            using var comando = cnx.CreateCommand();
            comando.CommandText = "SEL_PRODUCTO_SEARCH";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new SqlParameter("@NOMBRE", model.Nombre));
            cnx.Open();

            using DbDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                var data = new ProductoSearchResponse()
                {
                    Id = reader.GetInt32("ID"),
                    Nombre = reader.GetString("NOMBRE"),
                    PrecioUnitario = (double)reader.GetDecimal("PRECIO_UNITARIO"),
                    Moneda = reader.GetString("MONEDA")
                };
                productos.Add(data);
            }
            
            reader.Close();

            //paginacion
            int limit = model.Page * model.PageSize;
            int total = productos.Count();
            int mostrados = (model.PageSize * model.Page < total ? model.PageSize : total - ((model.PageSize * model.Page) - model.PageSize));
            var result = new List<ProductoSearchResponse>();
            if (total > 0)
            {
                if (total >= limit)
                {
                    result = productos.OrderByDescending(x => x.Nombre).Skip((model.Page - 1) * model.PageSize).Take(model.PageSize).ToList();
                }
                else
                {
                    result = productos.OrderByDescending(x => x.Nombre).Skip((model.Page - 1) * model.PageSize).Take(mostrados).ToList();
                }
            }
            else
            {
                result = productos.ToList();
            }

            PagedResponse<ProductoSearchResponse> response = new PagedResponse<ProductoSearchResponse>();
            response.Data = result;
            response.TotalRows = total;
            return response;
        }
    }
}

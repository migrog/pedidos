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
    public class ClienteRepositorio: RepositorioBase<Cliente>, IClienteRepositorio
    {
        public PagedResponse<ClienteSearchResponse> Search(ClienteSearchRequest model)
        {
            var db = context.Database;
            using var cnx = db.GetDbConnection();

            var clientes = new List<ClienteSearchResponse>();
            using var comando = cnx.CreateCommand();
            comando.CommandText = "SEL_CLIENTE_SEARCH";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new SqlParameter("@NOMBRE", model.Nombre));
            cnx.Open();

            using DbDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                var data = new ClienteSearchResponse()
                {
                    Id = reader.GetInt32("ID"),
                    Nombre = reader.GetString("NOMBRE"),
                };
                clientes.Add(data);
            }

            reader.Close();

            //paginacion
            int limit = model.Page * model.PageSize;
            int total = clientes.Count();
            int mostrados = (model.PageSize * model.Page < total ? model.PageSize : total - ((model.PageSize * model.Page) - model.PageSize));
            var result = new List<ClienteSearchResponse>();
            if (total > 0)
            {
                if (total >= limit)
                {
                    result = clientes.OrderByDescending(x => x.Nombre).Skip((model.Page - 1) * model.PageSize).Take(model.PageSize).ToList();
                }
                else
                {
                    result = clientes.OrderByDescending(x => x.Nombre).Skip((model.Page - 1) * model.PageSize).Take(mostrados).ToList();
                }
            }
            else
            {
                result = clientes.ToList();
            }

            PagedResponse<ClienteSearchResponse> response = new PagedResponse<ClienteSearchResponse>();
            response.Data = result;
            response.TotalRows = total;
            return response;
        }
    }
}

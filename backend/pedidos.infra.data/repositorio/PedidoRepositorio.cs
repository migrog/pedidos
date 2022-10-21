using System.Collections.Generic;
using System.Data;
using pedidos.dominio.entidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using pedidos.dominio.entidades.interfaces.repositorio;
using pedidos.dominio.entidades.dto;
using System;
using System.Data.Common;
using System.Linq;

namespace pedidos.infra.data.repositorio
{
    public class PedidoRepositorio : RepositorioBase<Pedido>, IPedidoRepositorio
    {
        public PagedResponse<PedidoSearchResponse> Search(PedidoSearchRequest model)
        {
            var db = context.Database;
            using var cnx = db.GetDbConnection();

            var pedidos = new List<PedidoSearchResponse>();
            using var comando = cnx.CreateCommand();
            comando.CommandText = "SEL_PEDIDO_SEARCH";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new SqlParameter("@FECHA_EMISION", model.FechaEmision == "" ? null : model.FechaEmision));
            cnx.Open();

            using DbDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                var data = new PedidoSearchResponse()
                {
                    Id = reader.GetInt32("ID"),
                    Cliente = reader.GetString("CLIENTE"),
                    Total = (double)reader.GetDecimal("TOTAL"),
                    Moneda = reader.GetString("MONEDA"),
                    FechaEmision = reader.GetDateTime("FECHA_EMISION")
                };
                pedidos.Add(data);
            }

            reader.Close();

            //paginacion
            int limit = model.Page * model.PageSize;
            int total = pedidos.Count();
            int mostrados = (model.PageSize * model.Page < total ? model.PageSize : total - ((model.PageSize * model.Page) - model.PageSize));
            var result = new List<PedidoSearchResponse>();
            if (total > 0)
            {
                if (total >= limit)
                {
                    result = pedidos.OrderByDescending(x => x.FechaEmision).Skip((model.Page - 1) * model.PageSize).Take(model.PageSize).ToList();
                }
                else
                {
                    result = pedidos.OrderByDescending(x => x.FechaEmision).Skip((model.Page - 1) * model.PageSize).Take(mostrados).ToList();
                }
            }
            else
            {
                result = pedidos.ToList();
            }

            PagedResponse<PedidoSearchResponse> response = new PagedResponse<PedidoSearchResponse>();
            response.Data = result;
            response.TotalRows = total;
            return response;
        }
        public PedidoGetResponse GetById(int id)
        {
            var ctx = context;
            var pedido = ctx.Pedido.Where(x=>x.Id.Equals(id)).FirstOrDefault();
            var cliente = ctx.Cliente.Where(x=>x.Id.Equals(pedido.IdCliente)).FirstOrDefault();
            var detalle = from d in ctx.PedidoDetalle.Where(x => x.IdPedido == id)
                          join p in ctx.Producto on d.IdProducto equals p.Id
                          select new PedidoDetalleGet()
                          {
                              IdProducto = p.Id,
                              NombreProducto = p.Nombre,
                              Cantidad = d.Cantidad,
                              PrecioUnitario = d.PrecioUnitario,
                              Total = d.Cantidad * d.PrecioUnitario
                          };

            var result = new PedidoGetResponse()
            {
                Pedido = new PedidoGet()
                {
                    Id = pedido.Id,
                    Cliente = cliente,
                    MonedaEnum = pedido.MonedaEnum,
                    FechaEmision = pedido.FechaEmision
                },
                Detalle = detalle.ToList()
            };

            return result;
        }
        public void Insert(Pedido pedido, List<PedidoDetalle> detalle)
        {
            var db = context.Database;
            using var cnx = db.GetDbConnection();
            using var comando = cnx.CreateCommand();
            comando.CommandText = "INS_PEDIDO";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new SqlParameter("ID_CLIENTE", pedido.IdCliente));
            comando.Parameters.Add(new SqlParameter("TOTAL", pedido.Total));
            comando.Parameters.Add(new SqlParameter("MONEDA_ENUM", pedido.MonedaEnum));
            comando.Parameters.Add(new SqlParameter("FECHA_EMISION", pedido.FechaEmision));

            DataTable table = new DataTable();
            table.Columns.Add("ID_PRODUCTO", typeof(int));
            table.Columns.Add("CANTIDAD", typeof(decimal));
            table.Columns.Add("PRECIO_UNITARIO", typeof(decimal));

            foreach(var item in detalle)
            {
                table.Rows.Add(item.IdProducto, item.Cantidad, item.PrecioUnitario);
            }

            comando.Parameters.Add(new SqlParameter("@DETALLE", table));
            cnx.Open();
            comando.ExecuteNonQuery();
        }

        public void Update(Pedido pedido, List<PedidoDetalle> detalle)
        {
            var db = context.Database;
            using var cnx = db.GetDbConnection();
            using var comando = cnx.CreateCommand();
            comando.CommandText = "UPD_PEDIDO";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new SqlParameter("ID", pedido.Id));
            comando.Parameters.Add(new SqlParameter("ID_CLIENTE", pedido.IdCliente));
            comando.Parameters.Add(new SqlParameter("TOTAL", pedido.Total));
            comando.Parameters.Add(new SqlParameter("MONEDA_ENUM", pedido.MonedaEnum));
            comando.Parameters.Add(new SqlParameter("FECHA_EMISION", pedido.FechaEmision));

            DataTable table = new DataTable();
            table.Columns.Add("ID_PRODUCTO", typeof(int));
            table.Columns.Add("CANTIDAD", typeof(decimal));
            table.Columns.Add("PRECIO_UNITARIO", typeof(decimal));

            foreach (var item in detalle)
            {
                table.Rows.Add(item.IdProducto, item.Cantidad, item.PrecioUnitario);
            }

            comando.Parameters.Add(new SqlParameter("@DETALLE", table));
            cnx.Open();
            comando.ExecuteNonQuery();
        }


    }
}

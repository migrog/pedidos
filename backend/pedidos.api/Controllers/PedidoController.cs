using System.Linq;
using pedidos.dominio.entidades;
using Microsoft.AspNetCore.Mvc;
using pedidos.dominio.core.servicios;
using pedidos.dominio.entidades.dto;
using pedidos.dominio.entidades.interfaces.servicios;
using pedidos.api.Models;
using System;
using Serilog;
using System.Collections.Generic;

namespace pedidos.api.Controllers
{
    [Route("api/pedidos")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _servicioPedido = new PedidoService();
        private readonly IServicioBase<PedidoDetalle> _servicioPedidoDetalle = new ServicioBase<PedidoDetalle>();

        [HttpPost("search")]
        public IActionResult Seach([FromBody] PedidoSearchRequest value)
        {
            try
            {
                var result = _servicioPedido.Search(value);

                return Ok(new BaseResponse() { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "PedidoController -> Seach");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] PedidoPostRequest value)
        {
            try
            {
                var pedido = new Pedido();
                pedido.IdCliente = value.Pedido.IdCliente;
                pedido.FechaEmision = value.Pedido.FechaEmision;
                pedido.MonedaEnum = value.Pedido.MonedaEnum;
                pedido.Total = value.Detalle.Sum(x => x.Cantidad * x.PrecioUnitario);

                var detalle = new List<PedidoDetalle>();
                foreach(var item in value.Detalle)
                {
                    detalle.Add(new PedidoDetalle()
                    {
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = item.PrecioUnitario
                    });
                }
                _servicioPedido.Post(pedido, detalle);

                return Ok(new BaseResponse() { Success = true, Data = "Se registró con éxito" });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "PedidoController -> Post");
                return BadRequest("Error al intentar registrar");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var cliente = _servicioPedido.Get().Where(x => x.Id == id).FirstOrDefault();

                if (cliente == null)
                    return NotFound("Recurso no encontrado");

                var result = _servicioPedido.GetById(id);

                return Ok(new BaseResponse() { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "PedidoController -> GetById");
                return BadRequest(ex.Message);

            }

        }

        [HttpPut]
        public IActionResult Put([FromBody] PedidoPutRequest value)
        {
            try
            {
                var pedido = _servicioPedido.Get().Where(x => x.Id == value.Pedido.Id).FirstOrDefault();
                if (pedido == null)
                    return NotFound("Recurso no encontrado");

                pedido.IdCliente = value.Pedido.IdCliente;
                pedido.FechaEmision = value.Pedido.FechaEmision;
                pedido.MonedaEnum = value.Pedido.MonedaEnum;
                pedido.Total = value.Detalle.Sum(x => x.Cantidad * x.PrecioUnitario);

                var detalle = new List<PedidoDetalle>();
                foreach (var item in value.Detalle)
                {
                    detalle.Add(new PedidoDetalle()
                    {
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = item.PrecioUnitario
                    });
                }

                _servicioPedido.Put(pedido, detalle);

                return Ok(new BaseResponse() { Success = true, Data = "Se actualizó con éxito" });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "PedidoController -> Put");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var pedido = _servicioPedido.Get().Where(x => x.Id == id).FirstOrDefault();
                if (pedido == null)
                    return NotFound("Recurso no encontrado");

                var detalle = _servicioPedidoDetalle.Get().Where(x => x.IdPedido == id);
                if (detalle.Count() > 0)
                    _servicioPedidoDetalle.Delete(detalle);
                
                _servicioPedido.Delete(id);

                return Ok(new BaseResponse() { Success = true, Data = "Se eliminó con éxito" });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "PedidoController -> Delete");
                return BadRequest(ex.Message);

            }

        }
    }
}

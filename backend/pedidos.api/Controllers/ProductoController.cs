using System;
using System.Linq;
using pedidos.api.Models;
using pedidos.dominio.core.servicios;
using pedidos.dominio.core.validadores;
using pedidos.dominio.entidades;
using pedidos.dominio.entidades.dto;
using pedidos.dominio.entidades.interfaces.servicios;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace pedidos.api.Controllers
{
    [Route("api/productos")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService = new ProductoService();

        [HttpPost("search")]
        public IActionResult Seach([FromBody] ProductoSearchRequest value)
        {
            try
            {
                var result = _productoService.Search(value);

                return Ok(new BaseResponse() { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ProductoController -> Seach");
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] ProductoPostRequest value)
        {
            try
            {
                var producto = new Producto();
                producto.Nombre = value.Nombre;
                producto.PrecioUnitario = value.PrecioUnitario;
                producto.MonedaEnum = value.MonedaEnum;

                _productoService.Post<ProductoValidator>(producto);

                return Ok(new BaseResponse() { Success = true, Data = "Se registró con éxito" });
            }
            catch(Exception ex)
            {
                Log.Error(ex, "ProductoController -> Post");
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var producto = _productoService.Get().Where(x => x.Id == id).FirstOrDefault();

                if (producto == null)
                    return NotFound("Recurso no encontrado");

                var result = new ProductoGetResponse() {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    PrecioUnitario = producto.PrecioUnitario,
                    MonedaEnum = producto.MonedaEnum
                };

                return Ok(new BaseResponse() { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ProductoController -> GetById");
                return BadRequest(ex.Message);

            }

        }
        
        [HttpPut]
        public IActionResult Put([FromBody] ProductoPutRequest value)
        {
            try
            {
                var producto = _productoService.Get().Where(x => x.Id == value.Id).FirstOrDefault();
                producto.Nombre = value.Nombre;
                producto.PrecioUnitario = value.PrecioUnitario;
                producto.MonedaEnum = value.MonedaEnum;

                _productoService.Put<ProductoValidator>(producto);

                return Ok(new BaseResponse() { Success = true, Data = "Se actualizó con éxito" });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ProductoController -> Put");
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var producto = _productoService.Get().Where(x => x.Id == id).FirstOrDefault();
                if (producto == null)
                    return NotFound("Recurso no encontrado");

                _productoService.Delete(id);

                return Ok(new BaseResponse() { Success = true, Data = "Se eliminó con éxito" });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ProductoController -> Delete");
                return BadRequest(ex.Message);

            }

        }
    }
}

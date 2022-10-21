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
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _servicioCliente = new ClienteService();

        [HttpPost("search")]
        public IActionResult Seach([FromBody] ClienteSearchRequest value)
        {
            try
            {
                var result = _servicioCliente.Search(value);

                return Ok(new BaseResponse() { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ClienteController -> Seach");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ClientePostRequest value)
        {
            try
            {
                var cliente = new Cliente();
                cliente.Nombre = value.Nombre;

                _servicioCliente.Post<ClienteValidator>(cliente);
                return Ok(new BaseResponse() { Success = true, Data = "Se registró con éxito" });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ClienteController -> Post");
                return BadRequest("Error al intentar registrar");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var cliente = _servicioCliente.Get().Where(x => x.Id == id).FirstOrDefault();

                if (cliente == null)
                    return NotFound("Recurso no encontrado");

                var result = new ClienteGetResponse()
                {
                    Id = cliente.Id,
                    Nombre = cliente.Nombre,
                };

                return Ok(new BaseResponse() { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ClienteController -> GetById");
                return BadRequest(ex.Message);

            }

        }

        [HttpPut]
        public IActionResult Put([FromBody] ClientePutRequest value)
        {
            try
            {
                var cliente = _servicioCliente.Get().Where(x => x.Id == value.Id).FirstOrDefault();
                cliente.Nombre = value.Nombre;


                _servicioCliente.Put<ClienteValidator>(cliente);

                return Ok(new BaseResponse() { Success = true, Data = "Se actualizó con éxito" });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ClienteController -> Put");
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var cliente = _servicioCliente.Get().Where(x => x.Id == id).FirstOrDefault();
                if (cliente == null)
                    return NotFound("Recurso no encontrado");

                _servicioCliente.Delete(id);

                return Ok(new BaseResponse() { Success = true, Data = "Se eliminó con éxito" });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ClienteController -> Delete");
                return BadRequest(ex.Message);

            }

        }
    }
}

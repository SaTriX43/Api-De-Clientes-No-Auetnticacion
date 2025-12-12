using API_de_Clientes__sin_autenticación_.DTOs;
using API_de_Clientes__sin_autenticación_.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_de_Clientes__sin_autenticación_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService) { 
            _clienteService = clienteService;
        }

        [HttpGet("obtener-cliente/{clienteId}")]
        public async Task<IActionResult> ObtenerCliente(int clienteId)
        {
            if(clienteId <= 0)
            {
                return BadRequest(new
                {
                    suceess = false,
                    error = "Su id no puede ser menor o igual a 0"
                });

            }

            var clienteEncontrado = await _clienteService.ObtenerCliente(clienteId);

            if(clienteEncontrado.IsFailure)
            {
                return NotFound(new
                {
                    success = false,
                    error = clienteEncontrado.Error
                });
            }

            return Ok(clienteEncontrado.Value);
        }

        [HttpGet("obtener-clientes")]
        public async Task<IActionResult> ObtenerClientes(
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFinal,
            [FromQuery] bool ordenarDeZ = false
        )
        {
            var clientesEncontrados = await _clienteService.ObtenerClientes(fechaInicio,fechaFinal,ordenarDeZ);
            return Ok(new
            {
                success = true,
                valor = clientesEncontrados.Value
            });
        }

        [HttpGet("obtener-cliente-email/{email}")] 
        public async Task<IActionResult> ObtenerClientePorEmail(string email)
        {
            var clientePorEmail = await _clienteService.ObtenerClientePorEmail(email);

            if (clientePorEmail.IsFailure) {
                return BadRequest(new
                {
                    success = false,
                    error = clientePorEmail.Error
                });
            }

            return Ok(clientePorEmail.Value);
        }

        [HttpPost("crear-cliente")]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteCrearDto clienteCrearDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ModelState
                });
            }

            var clienteCreado = await _clienteService.CrearCliente(clienteCrearDto);

            if(clienteCreado.IsFailure) 
            {
                return BadRequest(new
                {
                    success = false,
                    error = clienteCreado.Error
                });
            }

            return CreatedAtAction(
                nameof(ObtenerCliente),
                new {id = clienteCreado.Value.Id},
                clienteCreado.Value
                );
        }

        [HttpPut("actualizar-cliente/{id}")]
        public async Task<IActionResult> ActualizarCliente([FromBody] ClienteCrearDto clienteActualizar,int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ModelState
                });
            }

            var clienteActualizado = await _clienteService.ActualizarCliente(clienteActualizar, id);

            if(clienteActualizado.IsFailure)
            {
                return BadRequest(new
                {
                    success = false,
                    error = clienteActualizado.Error
                });
            }

            return Ok(new
            {
                success = true,
                valor = clienteActualizado.Value
            });
        }

        [HttpDelete("eliminar-cliente/{clienteId}")]
        public async Task<IActionResult> EliminarCliente(int clienteId)
        {
            if(clienteId <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    error = "El id no debe de ser menor o igual a 0"
                });
            }

            var clienteEliminado = await _clienteService.EliminarCliente(clienteId);

            if(clienteEliminado.IsFailure)
            {
                return NotFound(new
                {
                    success = false,
                    error = clienteEliminado.Error
                });
            }

            return NoContent();
        }
    }
}

using API_de_Clientes__sin_autenticación_.DTOs;
using API_de_Clientes__sin_autenticación_.Models;

namespace API_de_Clientes__sin_autenticación_.Services
{
    public interface IClienteService
    {
        public Task<Result<ClienteDto>> CrearCliente(ClienteCrearDto clienteCrearDto);
        public Task<Result<ClienteDto>> ObtenerCliente(int clienteId);
        public Task<Result<List<ClienteDto>>> ObtenerClientes(DateTime? fechaInicio, DateTime? fechaFinal, bool ordenarDeZ);
    }
}

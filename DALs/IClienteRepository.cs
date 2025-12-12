using API_de_Clientes__sin_autenticación_.DTOs;
using API_de_Clientes__sin_autenticación_.Models;

namespace API_de_Clientes__sin_autenticación_.DALs
{
    public interface IClienteRepository
    {
        public Task<Cliente?> ObtenerClientePorEmail(string email);
        public Task<Cliente?> ObtenerCliente(int clienteId);
        public Task<List<Cliente>> ObtenerClientes(DateTime? fechaInicio, DateTime? fechaFinal, bool ordenarDeZ);
        public Task<Cliente> CrearCliente(Cliente cliente);
        public Task<Cliente> ActualizarCliente(Cliente clienteActualizar, int id);
        public Task EliminarCliente(int clienteId);
    }
}

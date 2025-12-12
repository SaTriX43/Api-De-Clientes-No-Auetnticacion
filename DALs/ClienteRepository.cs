using API_Clientes.Data;
using API_de_Clientes__sin_autenticación_.DTOs;
using API_de_Clientes__sin_autenticación_.Models;
using Microsoft.EntityFrameworkCore;

namespace API_de_Clientes__sin_autenticación_.DALs
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente?> ObtenerClientePorEmail(string email)
        {
            var clienteEncontrado = await _context.Clientes.FirstOrDefaultAsync(c => c.Email == email);
            return clienteEncontrado;
        }
        public async Task<Cliente?> ObtenerCliente(int clienteId)
        {
            var clienteEncontrado = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == clienteId);
            return clienteEncontrado;
        }
        public async Task<List<Cliente>> ObtenerClientes(DateTime? fechaInicio, DateTime? fechaFinal, bool ordenarDeZ)
        {
            var query = _context.Clientes.AsQueryable();

            if(fechaInicio.HasValue)
            {
                query = query.Where(c => c.FechaDeRegistro >= fechaInicio.Value);
            }

            if (fechaFinal.HasValue)
            {
                query = query.Where(c => c.FechaDeRegistro <= fechaFinal.Value);
            }

            if(ordenarDeZ)
            {
                query = query.OrderByDescending(c => c.Nombre);
            }
            else
            {
                query = query.OrderBy(c => c.Nombre);
            }

            return await query.ToListAsync();
        }
        public async Task<Cliente> CrearCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
        public async Task<Cliente> ActualizarCliente(ClienteCrearDto clienteActualizar, int id)
        {
            var clienteEncontrado = await _context.Clientes.FirstOrDefaultAsync(c  => c.Id == id);

            clienteEncontrado.Nombre = clienteActualizar.Nombre;
            clienteEncontrado.Email = clienteActualizar.Email;

            await _context.SaveChangesAsync();

            return clienteEncontrado;
        }
        public async Task EliminarCliente(int clienteId)
        {
            var clienteEncontrado = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == clienteId);

            _context.Remove(clienteEncontrado);
            await _context.SaveChangesAsync();
        }
    }
}

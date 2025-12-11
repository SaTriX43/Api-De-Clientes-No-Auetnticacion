using API_de_Clientes__sin_autenticación_.DALs;
using API_de_Clientes__sin_autenticación_.DTOs;
using API_de_Clientes__sin_autenticación_.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API_de_Clientes__sin_autenticación_.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository) { 
            _clienteRepository = clienteRepository;
        }

        public async Task<Result<ClienteDto>> ObtenerCliente(int clienteId)
        {
            var clienteEncontrado = await _clienteRepository.ObtenerCliente(clienteId);

            if (clienteEncontrado == null) {
                return Result<ClienteDto>.Failure($"El cliente con id = {clienteId} no existe");
            }

            var clienteEncontradoDto = new ClienteDto
            {
                Id = clienteEncontrado.Id,
                Nombre = clienteEncontrado.Nombre,
                Email = clienteEncontrado.Email,
                FechaDeRegistro = clienteEncontrado.FechaDeRegistro 
            };

            return Result<ClienteDto>.Success(clienteEncontradoDto);
        }
        public async Task<Result<List<ClienteDto>>> ObtenerClientes(DateTime? fechaInicio, DateTime? fechaFinal, bool ordenarDeZ)
        {
            var clientesEncontrados = await _clienteRepository.ObtenerClientes(fechaInicio,fechaFinal,ordenarDeZ);

            var clientesEncontradosDto = clientesEncontrados.Select(c => new ClienteDto
            {
               Id = c.Id,
               Nombre = c.Nombre,
               Email = c.Email,
               FechaDeRegistro = c.FechaDeRegistro
            }).ToList();

            return Result<List<ClienteDto>>.Success(clientesEncontradosDto);
        }
        public async Task<Result<ClienteDto>> CrearCliente(ClienteCrearDto clienteCrearDto)
        {
            var clienteEncontradoEmail = await _clienteRepository.ObtenerClientePorEmail(clienteCrearDto.Email);

            if (clienteEncontradoEmail != null) {
                return Result<ClienteDto>.Failure("Cliente con email ya existente");
            }

            var normalizarEmail = clienteCrearDto.Email.Trim().ToLower();

            var clienteModel = new Cliente
            {
                Nombre = clienteCrearDto.Nombre,
                Email = normalizarEmail,
            };

            var clienteCreado = await _clienteRepository.CrearCliente(clienteModel);

            var clienteCreadoDto = new ClienteDto
            {
                Id = clienteCreado.Id,
                Email = clienteCreado.Email,
                Nombre = clienteCreado.Nombre,
                FechaDeRegistro = clienteCreado.FechaDeRegistro
            };

            return Result<ClienteDto>.Success(clienteCreadoDto);
        }
    }
}

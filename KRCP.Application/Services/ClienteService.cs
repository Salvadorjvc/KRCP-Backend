using KRCP.Application.DTOs.Cliente;
using KRCP.Application.Interfaces.Repositories;
using KRCP.Application.Interfaces.Services;
using KRCP.Domain.Exceptions;
using KRCP.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteResponseDto> GetByIdAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);

            if (cliente is null)
            {
                throw new EntityNotFoundException("Cliente", id);
            }

            return cliente.Adapt<ClienteResponseDto>();
        }

        public async Task<IReadOnlyList<ClienteResponseDto>> GetAllAsync()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return clientes.Adapt<List<ClienteResponseDto>>();
        }

        public async Task<ClienteResponseDto> CreateAsync(ClienteCreateRequestDto dto)
        {
            await ValidarRucUnicoAsync(dto.RUC);

            var cliente = dto.Adapt<Cliente>();

            await _clienteRepository.AddAsync(cliente);
            return cliente.Adapt<ClienteResponseDto>();
        }

        public async Task UpdateAsync(int id, ClienteUpdateRequestDto dto)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente is null)
            {
                throw new EntityNotFoundException("Cliente", id);
            }

            dto.Adapt(cliente);
            cliente.FechaModificacion = DateTime.UtcNow;

            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task ChangeStatusAsync(int id, bool activo)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);

            if (cliente is null)
            {
                throw new EntityNotFoundException("Cliente", id);
            }

            cliente.Activo = activo;
            cliente.FechaModificacion = DateTime.UtcNow;

            await _clienteRepository.UpdateAsync(cliente);
        }

        private async Task ValidarRucUnicoAsync(string ruc)
        {
            var existeRuc = await _clienteRepository.ExistsByRucAsync(ruc);
            if (existeRuc)
            {
                throw new DuplicateEntityException("un Cliente", "el RUC", ruc);
            }
        }



    }
}

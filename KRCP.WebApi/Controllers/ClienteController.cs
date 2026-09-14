using KRCP.Application.DTOs.Cliente;
using KRCP.Application.DTOs.Common;
using KRCP.Application.Interfaces.Services;
using KRCP.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KRCP.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // este authorize solo, solo exige estar logeado sin rol especifico
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ClienteResponseDto>>> GetAll()
        { 
            var Clientes = await _clienteService.GetAllAsync();
            return Ok(Clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteResponseDto>> GetById(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            return Ok(cliente);
        }

        
        [HttpPost]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Planificador}")]
        public async Task<ActionResult<ClienteResponseDto>> Create(ClienteCreateRequestDto dto)
        {
            var resultado = await _clienteService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = resultado.ClienteId }, resultado);
        }

        
        [HttpPut("{id}")]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Planificador}")]
        public async Task<IActionResult> Update(int id, ClienteUpdateRequestDto dto)
        {
            await _clienteService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpPatch("{id}/estado")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ChangeStatus(int id, ChangeStatusRequestDto dto)
        {
            await _clienteService.ChangeStatusAsync(id, dto.Activo);
            return NoContent();
        }
    }
}

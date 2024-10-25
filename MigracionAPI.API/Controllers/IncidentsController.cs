using Microsoft.AspNetCore.Mvc;
using MigracionAPI.Domain.DTOs;
using MigracionAPI.Domain.Entities;
using MigracionAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MigracionAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidentsController : ControllerBase

    /// <summary>
    /// Registra un nuevo incidente
    /// By Jason Mendoza 20231889
    /// </summary>
    /// <param name="dto">Datos del incidente</param>
    /// <param name="agentId">ID del agente que registra el incidente</param>
    /// <returns>ID de confirmación del incidente</returns>
    /// <response code="200">Incidente registrado exitosamente</response>
    /// <response code="404">Agente no encontrado</response>
    /// <response code="400">Datos inválidos</response>

    {
        private readonly ApplicationDbContext _context;

        public IncidentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterIncident([FromBody] IncidentRegistrationDto dto, [FromQuery] int agentId)
        {
            var agent = await _context.Agents.FindAsync(agentId);
            if (agent == null)
                return NotFound("Agente no encontrado");

            var incident = new Incident
            {
                Pasaporte = dto.Pasaporte,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                WhatsApp = dto.WhatsApp,
                FechaNacimiento = dto.FechaNacimiento,
                Latitud = dto.Latitud,
                Longitud = dto.Longitud,
                AgentId = agentId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();

            return Ok(new { 
                message = "Incidente registrado exitosamente",
                incidentId = incident.Id
            });
        }

        [HttpGet("agent/{agentId}")]
        public async Task<IActionResult> GetAgentIncidents(int agentId)
        {
            var incidents = await _context.Incidents
                .Where(i => i.AgentId == agentId)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();

            return Ok(incidents);
        }
    }
}
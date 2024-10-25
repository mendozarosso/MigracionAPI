// el controlador de los agentes

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MigracionAPI.Domain.DTOs;
using MigracionAPI.Domain.Entities;
using MigracionAPI.Infrastructure.Data;
using System.Security.Cryptography;
using System.Text;

namespace MigracionAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentsController : ControllerBase

     /// <summary>
     /// By Jason Mendoza 20231889
    /// Registra un nuevo agente de migración
    /// </summary>
    /// <param name="dto">Datos del agente a registrar</param>
    /// <returns>Información del agente registrado</returns>
    /// <response code="200">Agente registrado </response>
    /// <response code="400">Datos inválidos o agente ya existe</response>
    {
        private readonly ApplicationDbContext _context;

        public AgentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AgentRegistrationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _context.Agents.AnyAsync(a => a.Cedula == dto.Cedula || a.Email == dto.Email))
            {
                return BadRequest("Ya existe un agente con esta cédula o email");
            }

            var agent = new Agent
            {
                Cedula = dto.Cedula,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Telefono = dto.Telefono,
                Email = dto.Email,
                PasswordHash = ComputeHash(dto.Password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Agents.Add(agent);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Agente registrado exitosamente", agentId = agent.Id });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var passwordHash = ComputeHash(dto.Password);

            var agent = await _context.Agents
                .FirstOrDefaultAsync(a => (a.Cedula == dto.Identifier || a.Email == dto.Identifier) 
                                        && a.PasswordHash == passwordHash);

            if (agent == null)
            {
                return Unauthorized("Credenciales inválidas");
            }

            return Ok(new
            {
                agent.Id,
                agent.Cedula,
                agent.Nombre,
                agent.Apellido,
                agent.Email
            });
        }

        private static string ComputeHash(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
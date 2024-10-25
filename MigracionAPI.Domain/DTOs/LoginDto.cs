using System.ComponentModel.DataAnnotations;

namespace MigracionAPI.Domain.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El identificador es obligatorio")]
        public string Identifier { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; } = string.Empty;
    }
}
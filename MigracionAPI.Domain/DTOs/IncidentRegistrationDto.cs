using System.ComponentModel.DataAnnotations;

namespace MigracionAPI.Domain.DTOs
{
    public class IncidentRegistrationDto
    {
        [Required(ErrorMessage = "El pasaporte es obligatorio")]
        public string Pasaporte { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; } = string.Empty;

        public string WhatsApp { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La latitud es obligatoria")]
        public decimal Latitud { get; set; }

        [Required(ErrorMessage = "La longitud es obligatoria")]
        public decimal Longitud { get; set; }
    }
}
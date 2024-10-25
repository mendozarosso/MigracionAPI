namespace MigracionAPI.Domain.Entities
{
    public class Incident
    {
        public int Id { get; set; }
        public string Pasaporte { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string WhatsApp { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AgentId { get; set; }
        public virtual Agent? Agent { get; set; }
    }
}
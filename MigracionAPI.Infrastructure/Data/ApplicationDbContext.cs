using Microsoft.EntityFrameworkCore;
using MigracionAPI.Domain.Entities;

namespace MigracionAPI.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para la entidad Agent
            modelBuilder.Entity<Agent>(entity =>
            {
                entity.ToTable("Agents");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PasswordHash)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");

                // Índices únicos
                entity.HasIndex(e => e.Cedula).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Configuración para la entidad Incident
            modelBuilder.Entity<Incident>(entity =>
            {
                entity.ToTable("Incidents");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Pasaporte)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.WhatsApp)
                    .HasMaxLength(20);

                entity.Property(e => e.FechaNacimiento)
                    .IsRequired();

                entity.Property(e => e.Latitud)
                    .HasPrecision(18, 6)
                    .IsRequired();

                entity.Property(e => e.Longitud)
                    .HasPrecision(18, 6)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");

                // Relación con Agent
                entity.HasOne(e => e.Agent)
                    .WithMany(a => a.Incidents)
                    .HasForeignKey(e => e.AgentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
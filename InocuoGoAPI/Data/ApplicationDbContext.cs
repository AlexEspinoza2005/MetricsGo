using Microsoft.EntityFrameworkCore;
using InocuoGoMetrics.API.Models;

namespace InocuoGoMetrics.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Organizacion> Organizaciones { get; set; }
        public DbSet<Chatbot> Chatbots { get; set; }
        public DbSet<Canal> Canales { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioAdmin> UsuariosAdmin { get; set; }
        public DbSet<Conversacion> Conversaciones { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }
        public DbSet<Topico> Topicos { get; set; }
        public DbSet<Subcategoria> Subcategorias { get; set; }
        public DbSet<Clasificacion> Clasificaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Clave compuesta para Clasificaciones
            modelBuilder.Entity<Clasificacion>()
                .HasKey(c => new { c.IdMenCla, c.IdSubCla });

            // Índices únicos
            modelBuilder.Entity<Organizacion>()
                .HasIndex(o => o.NombreOrg)
                .IsUnique();

            modelBuilder.Entity<UsuarioAdmin>()
                .HasIndex(u => u.CorreoAdm)
                .IsUnique();

            modelBuilder.Entity<Canal>()
                .HasIndex(c => c.NombreCan)
                .IsUnique();

            // Unique constraint para Usuario Final
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => new { u.IdOrgUsu, u.IdCanUsu, u.IdCanalUsu })
                .IsUnique();

            // Unique constraint para Chatbot
            modelBuilder.Entity<Chatbot>()
                .HasIndex(c => new { c.IdOrgBot, c.NombreBot })
                .IsUnique();

            // Unique constraint para Tema
            modelBuilder.Entity<Topico>()
                .HasIndex(t => new { t.IdOrgTem, t.NombreTem })
                .IsUnique();

            // Unique constraint para Subcategoría
            modelBuilder.Entity<Subcategoria>()
                .HasIndex(s => new { s.IdTemSub, s.NombreSub })
                .IsUnique();
        }
    }
}
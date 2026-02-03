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

        // DbSets para cada tabla
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

            // Configuraciones adicionales si son necesarias

            // Índices únicos
            modelBuilder.Entity<Organizacion>()
                .HasIndex(o => o.NombreOrg)
                .IsUnique();

            modelBuilder.Entity<Canal>()
                .HasIndex(c => c.NombreCan)
                .IsUnique();

            // Relaciones (ya están definidas en los modelos con [ForeignKey], pero se pueden reforzar aquí)

            // Organización -> Chatbots
            modelBuilder.Entity<Chatbot>()
                .HasOne(c => c.Organizacion)
                .WithMany(o => o.Chatbots)
                .HasForeignKey(c => c.IdOrgBot)
                .OnDelete(DeleteBehavior.Restrict);

            // Usuario -> Conversaciones
            modelBuilder.Entity<Conversacion>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Conversaciones)
                .HasForeignKey(c => c.IdUsuCon)
                .OnDelete(DeleteBehavior.Restrict);

            // Chatbot -> Conversaciones
            modelBuilder.Entity<Conversacion>()
                .HasOne(c => c.Chatbot)
                .WithMany(b => b.Conversaciones)
                .HasForeignKey(c => c.IdBotCon)
                .OnDelete(DeleteBehavior.Restrict);

            // Conversación -> Mensajes
            modelBuilder.Entity<Mensaje>()
                .HasOne(m => m.Conversacion)
                .WithMany(c => c.Mensajes)
                .HasForeignKey(m => m.IdConMen)
                .OnDelete(DeleteBehavior.Cascade);

            // Tópico -> Subcategorías
            modelBuilder.Entity<Subcategoria>()
                .HasOne(s => s.Topico)
                .WithMany(t => t.Subcategorias)
                .HasForeignKey(s => s.IdTemSub)
                .OnDelete(DeleteBehavior.Restrict);

            // Clasificaciones (relación con Mensaje y Subcategoría)
            modelBuilder.Entity<Clasificacion>()
                .HasOne(c => c.Mensaje)
                .WithMany(m => m.Clasificaciones)
                .HasForeignKey(c => c.IdMenCla)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Clasificacion>()
                .HasOne(c => c.Subcategoria)
                .WithMany(s => s.Clasificaciones)
                .HasForeignKey(c => c.IdSubCla)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
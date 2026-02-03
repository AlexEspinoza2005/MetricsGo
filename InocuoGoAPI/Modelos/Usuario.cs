using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InocuoGoMetrics.API.Models
{
    [Table("usuarios_finales")]
    public class Usuario
    {
        [Key]
        [Column("idusu")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdUsu { get; set; } // BIGSERIAL

        [Required]
        [Column("idorg_usu")]
        public Guid IdOrgUsu { get; set; }

        [Required]
        [Column("idcan_usu")]
        public short IdCanUsu { get; set; }

        [Required]
        [Column("idcanalusu")]
        public string IdCanalUsu { get; set; } = string.Empty; // ID del canal externo (ej: número WhatsApp)

        [Column("nombreusu")]
        public string? NombreUsu { get; set; }

        [Column("activousu")]
        public bool ActivoUsu { get; set; } = true;

        [Column("creadousu")]
        public DateTime CreadoUsu { get; set; } = DateTime.UtcNow;

        // Relaciones
        [ForeignKey("IdOrgUsu")]
        public Organizacion? Organizacion { get; set; }

        [ForeignKey("IdCanUsu")]
        public Canal? Canal { get; set; }

        public ICollection<Conversacion>? Conversaciones { get; set; }
    }
}
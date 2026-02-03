// Models/Usuario.cs (para usuarios_finales)
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InocuoGoMetrics.API.Models
{
    [Table("usuarios_finales")]
    public class Usuario
    {
        [Key]
        [Column("idusu")]
        public int IdUsu { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombreusu")]
        public string NombreUsu { get; set; } = string.Empty;

        [Column("idcan_usu")]
        public int IdCanUsu { get; set; }

        [Column("idorg_usu")]
        public int IdOrgUsu { get; set; }

        [Column("activousu")]
        public bool ActivoUsu { get; set; } = true;

        [Column("creadousu")]
        public DateTime CreadoUsu { get; set; } = DateTime.UtcNow;

        // Relaciones
        [ForeignKey("IdCanUsu")]
        public Canal? Canal { get; set; }

        [ForeignKey("IdOrgUsu")]
        public Organizacion? Organizacion { get; set; }

        public ICollection<Conversacion>? Conversaciones { get; set; }
    }
}

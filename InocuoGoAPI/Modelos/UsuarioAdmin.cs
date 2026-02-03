using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InocuoGoMetrics.API.Models
{
    [Table("usuarios_admin")]
    public class UsuarioAdmin
    {
        [Key]
        [Column("idadm")]
        public int IdAdm { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombreadm")]
        public string NombreAdm { get; set; } = string.Empty;

        [Required]
        [Column("idorg_adm")]
        public int IdOrgAdm { get; set; }

        [StringLength(100)]
        [Column("passwadm")]
        public string? PassAdm { get; set; }

        [StringLength(50)]
        [Column("logintadm")]
        public string? LoginAdm { get; set; }

        [Column("creadoadm")]
        public DateTime CreadoAdm { get; set; } = DateTime.UtcNow;

        // Relaciones
        [ForeignKey("IdOrgAdm")]
        public Organizacion? Organizacion { get; set; }
    }
}
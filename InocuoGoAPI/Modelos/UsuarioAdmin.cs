using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InocuoGoMetrics.API.Models
{
    [Table("usuarios_admin")]
    public class UsuarioAdmin
    {
        [Key]
        [Column("idadm")]
        public Guid IdAdm { get; set; } = Guid.NewGuid();

        [Required]
        [Column("idorg_adm")]
        public Guid IdOrgAdm { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombreadm")]
        public string NombreAdm { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Column("correoadm")]
        public string CorreoAdm { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("passwadm")]
        public string PasswAdm { get; set; } = string.Empty;

        [Column("logintadm")]
        public DateTime? LogintAdm { get; set; } 

        [Column("creadoadm")]
        public DateTime CreadoAdm { get; set; } = DateTime.UtcNow;

        [ForeignKey("IdOrgAdm")]
        public Organizacion? Organizacion { get; set; }
    }
}
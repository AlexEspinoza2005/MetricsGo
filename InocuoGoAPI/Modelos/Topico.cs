using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InocuoGoMetrics.API.Models
{
    [Table("temas")]
    public class Topico
    {
        [Key]
        [Column("idtem")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdTem { get; set; } // BIGSERIAL

        [Required]
        [Column("idorg_tem")]
        public Guid IdOrgTem { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombretem")]
        public string NombreTem { get; set; } = string.Empty;

        [StringLength(500)]
        [Column("descritem")]
        public string? DescriTem { get; set; }

        [Column("activotem")]
        public bool ActivoTem { get; set; } = true;

        [Column("creadotem")]
        public DateTime CreadoTem { get; set; } = DateTime.UtcNow;

        // Relaciones
        [ForeignKey("IdOrgTem")]
        public Organizacion? Organizacion { get; set; }

        public ICollection<Subcategoria>? Subcategorias { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InocuoGoMetrics.API.Models
{
    [Table("temas")]
    public class Topico
    {
        [Key]
        [Column("idtem")]
        public int IdTem { get; set; }

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
        public ICollection<Subcategoria>? Subcategorias { get; set; }
    }
}
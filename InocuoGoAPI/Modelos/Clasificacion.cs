using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InocuoGoMetrics.API.Models
{
    [Table("clasificaciones")]
    public class Clasificacion
    {
        [Key, Column("idmen_cla", Order = 0)]
        public long IdMenCla { get; set; }

        [Key, Column("idsub_cla", Order = 1)]
        public long IdSubCla { get; set; }

        [Column("confianzacla")]
        public decimal? ConfianzaCla { get; set; }

        // Relaciones
        [ForeignKey("IdMenCla")]
        public Mensaje? Mensaje { get; set; }

        [ForeignKey("IdSubCla")]
        public Subcategoria? Subcategoria { get; set; }
    }
}
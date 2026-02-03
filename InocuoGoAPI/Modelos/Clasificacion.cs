using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InocuoGoMetrics.API.Models
{
    [Table("clasificaciones")]
    public class Clasificacion
    {
        [Key]
        [Column("idmen_cla")]
        public int IdMenCla { get; set; }

        [Column("idsub_cla")]
        public int IdSubCla { get; set; }

        [Column("confianza")]
        public decimal? Confianza { get; set; }

        // Relaciones
        [ForeignKey("IdMenCla")]
        public Mensaje? Mensaje { get; set; }

        [ForeignKey("IdSubCla")]
        public Subcategoria? Subcategoria { get; set; }
    }
}
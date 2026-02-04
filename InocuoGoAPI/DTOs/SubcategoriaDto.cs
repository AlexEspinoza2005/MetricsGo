using System.ComponentModel.DataAnnotations;
namespace InocuoGoMetrics.API.DTOs
{
    public class SubcategoriaCreateDto
    {
        [Required]
        [StringLength(100)]
        public string NombreSub { get; set; } = string.Empty;

        [StringLength(500)]
        public string? DescriSub { get; set; }

        [Required]
        public long IdTemSub { get; set; } 
    }

    public class SubcategoriaUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string NombreSub { get; set; } = string.Empty;

        [StringLength(500)]
        public string? DescriSub { get; set; }

        [Required]
        public long IdTemSub { get; set; } 

        public bool ActivoSub { get; set; }
    }
}
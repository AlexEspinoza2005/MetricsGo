using System.ComponentModel.DataAnnotations;
// DTOs/SubcategoriaDto.cs
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
        public long IdTemSub { get; set; } // Cambiado a long
    }

    public class SubcategoriaUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string NombreSub { get; set; } = string.Empty;

        [StringLength(500)]
        public string? DescriSub { get; set; }

        [Required]
        public long IdTemSub { get; set; } // Cambiado a long

        public bool ActivoSub { get; set; }
    }
}
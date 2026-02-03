// DTOs/TopicoDto.cs
using System.ComponentModel.DataAnnotations;

// DTOs/TopicoDto.cs
namespace InocuoGoMetrics.API.DTOs
{
    public class TopicoCreateDto
    {
        [Required]
        [StringLength(100)]
        public string NombreTem { get; set; } = string.Empty;

        [StringLength(500)]
        public string? DescriTem { get; set; }

        [Required]
        public Guid IdOrgTem { get; set; }  // ✅ Guid
    }

    public class TopicoUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string NombreTem { get; set; } = string.Empty;

        [StringLength(500)]
        public string? DescriTem { get; set; }

        [Required]
        public Guid IdOrgTem { get; set; }  // ✅ Guid

        public bool ActivoTem { get; set; }
    }
}
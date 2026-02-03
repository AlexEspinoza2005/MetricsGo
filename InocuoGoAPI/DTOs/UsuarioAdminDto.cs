// DTOs/UsuarioAdminDto.cs
using System.ComponentModel.DataAnnotations;

namespace InocuoGoMetrics.API.DTOs
{
    public class UsuarioAdminCreateDto
    {
        [Required]
        [StringLength(100)]
        public string NombreAdm { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string CorreoAdm { get; set; } = string.Empty;

        [Required]
        public Guid IdOrgAdm { get; set; }

        [Required]
        [StringLength(100)]
        public string PassAdm { get; set; } = string.Empty;

        // NO hay LoginAdm aquí - en la BD es DateTime? para timestamp de último login
    }

    public class UsuarioAdminUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string NombreAdm { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string CorreoAdm { get; set; } = string.Empty;

        [Required]
        public Guid IdOrgAdm { get; set; }

        [StringLength(100)]
        public string? PassAdm { get; set; }

        // NO hay LoginAdm aquí
    }
}
// DTOs/LoginRequestDto.cs
namespace InocuoGoMetrics.API.DTOs
{
    public class LoginRequestDto
    {
        public string Login { get; set; } = string.Empty; // Será el correo
        public string Password { get; set; } = string.Empty;
    }
}
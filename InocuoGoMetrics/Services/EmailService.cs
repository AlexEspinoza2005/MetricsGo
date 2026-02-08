using System.Net;
using System.Net.Mail;

namespace InocuoGoMetrics.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> EnviarCorreo(string destino, string asunto, string mensajeHtml)
        {
            try
            {
                var settings = _configuration.GetSection("SmtpSettings");

                Console.WriteLine($"📧 Enviando correo a: {destino}");
                Console.WriteLine($"📧 Asunto: {asunto}");

                var mail = new MailMessage
                {
                    From = new MailAddress(settings["SenderEmail"], settings["SenderName"]),
                    Subject = asunto,
                    Body = mensajeHtml,
                    IsBodyHtml = true
                };

                mail.To.Add(destino);

                using (var smtp = new SmtpClient(settings["Server"], int.Parse(settings["Port"])))
                {
                    smtp.Credentials = new NetworkCredential(settings["SenderEmail"], settings["Password"]);
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;

                    Console.WriteLine("📧 Conectando al servidor SMTP...");
                    await smtp.SendMailAsync(mail);
                    Console.WriteLine("✅ Correo enviado exitosamente");
                }

                return true;
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"❌ SMTP ERROR: {smtpEx.StatusCode} - {smtpEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR GENERAL: {ex.Message}");
                return false;
            }
        }
    }
}
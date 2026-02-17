using SendGrid;
using SendGrid.Helpers.Mail;

namespace InocuoGoMetrics.Services
{
    public class SendGridEmailService
    {
        private readonly string _apiKey;
        private readonly string _senderEmail;
        private readonly string _senderName;

        public SendGridEmailService(IConfiguration configuration)
        {
            var settings = configuration.GetSection("SendGridSettings");
            _apiKey = settings["ApiKey"];
            _senderEmail = settings["SenderEmail"];
            _senderName = settings["SenderName"];
        }

        public async Task<bool> EnviarCorreo(string destino, string asunto, string mensajeHtml)
        {
            try
            {
                var client = new SendGridClient(_apiKey);
                var from = new EmailAddress(_senderEmail, _senderName);
                var to = new EmailAddress(destino);
                var msg = MailHelper.CreateSingleEmail(from, to, asunto, "", mensajeHtml);

                var response = await client.SendEmailAsync(msg);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error enviando correo: {ex.Message}");
                return false;
            }
        }
    }
} 
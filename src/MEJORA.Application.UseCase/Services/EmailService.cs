using MailKit.Net.Smtp;
using MailKit.Security;
using MEJORA.Application.Dtos.Email.Request;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace MEJORA.Application.UseCase.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(EmailRequest request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("Email:SmtpUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.Para));
            email.Subject = request.Asunto;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = request.Contenido,
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(
                _configuration.GetSection("Email:SmtpHost").Value,
                Convert.ToInt32(_configuration.GetSection("Email:SmtpPort").Value),
                SecureSocketOptions.StartTls
            );

            await smtp.AuthenticateAsync(_configuration.GetSection("Email:SmtpUsername").Value, _configuration.GetSection("Email:SmtpPassword").Value);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}

using MEJORA.Application.Dtos.Email.Request;

namespace MEJORA.Application.UseCase.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest request);
    }
}

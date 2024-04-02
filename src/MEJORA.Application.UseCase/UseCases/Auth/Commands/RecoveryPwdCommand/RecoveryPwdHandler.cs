using MediatR;
using MEJORA.Application.Dtos.Email.Request;
using MEJORA.Application.Dtos.UserPerson.Request;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;
using MEJORA.Application.UseCase.Services;
using System.Text;

namespace MEJORA.Application.UseCase.UseCases.Auth.Commands.RecoveryPwdCommand
{
    public class RecoveryPwdHandler : IRequestHandler<RecoveryPwdCommand, Response<bool>>
    {
        private readonly IUserPersonRespository _userPersonRespository;
        private readonly IEmailService _emailService;
        public RecoveryPwdHandler(IUserPersonRespository userPersonRespository, IEmailService emailService)
            => (_userPersonRespository, _emailService) = (userPersonRespository, emailService);

        public async Task<Response<bool>> Handle(RecoveryPwdCommand request, CancellationToken cancellationToken)
        {
            var userPerson = await _userPersonRespository.GetUserPersonByEmail(new GetUserPersonByEmailRequest { Email = request.Email });

            if (userPerson == null) throw new Exception("El correo ingresado no existe.");

            string passwordRandom = GenerateRandomPassword(10);
            var password = BCrypt.Net.BCrypt.HashPassword(passwordRandom);

            //actualizamos una nueva contraseña
            var updatePwd = await _userPersonRespository.UpdateRecoveryPwd(new UpdateRecoveryPasswordRequest { UserPersonId = userPerson.Id, Password = password });

            if (!updatePwd)
            {
                throw new Exception("No se pudo recuperar contraseña. Contacta con soporte.");
            }

            EmailRequest EmailRq = new EmailRequest();
            EmailRq.Para = request.Email;
            EmailRq.Asunto = "Recuperación de cuenta.";
            EmailRq.Contenido = "<p>Correo : </p>" + userPerson.Email + "\n<p>Usuario : </p>" + userPerson.Username + "\n<p>Nueva Contraseña : </p>" + passwordRandom;

            // Enviar correo electrónico de confirmación
            await _emailService.SendEmailAsync(EmailRq);

            return new Response<bool>(true, "Se envió el correo para recoperación de contraseña.");
        }

        static string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            var passwordBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                passwordBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return passwordBuilder.ToString();
        }
    }
}

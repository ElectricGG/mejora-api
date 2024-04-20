using MediatR;
using MEJORA.Application.Dtos.Email.Request;
using MEJORA.Application.Dtos.UserPerson.Request;
using MEJORA.Application.Dtos.UserPerson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;
using MEJORA.Application.UseCase.Services;
using Microsoft.Extensions.Configuration;

namespace MEJORA.Application.UseCase.UseCases.UserPerson.CreateCommand
{
    public class CreateUserPersonHandler : IRequestHandler<CreateUserPersonCommand, Response<CreateUserPersonResponse>>
    {
        private readonly IUserPersonRespository _userPersonRespository;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public CreateUserPersonHandler(IUserPersonRespository userPersonRespository, IEmailService emailService, IConfiguration configuration)
            => (_userPersonRespository, _emailService, _configuration) = (userPersonRespository, emailService, configuration);

        public async Task<Response<CreateUserPersonResponse>> Handle(CreateUserPersonCommand command, CancellationToken cancellationToken)
        {
            var userPersonEmailExist = await _userPersonRespository.GetUserPersonByEmail(new GetUserPersonByEmailRequest { Email = command.Email });
            var userPersonUsernameExist = await _userPersonRespository.GetUserPersonByUsername(new GetUserPersonByUsernameRequest { Username = command.UserName });

            if (userPersonEmailExist != null) throw new Exception("El correo ingresado y esta registrado.");
            if (userPersonUsernameExist != null) throw new Exception("El usuario ingresado y esta registrado.");

            var password = BCrypt.Net.BCrypt.HashPassword(command.Password);
            var request = new CreateUserPersonRequest
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                UserName = command.UserName,
                Email = command.Email,
                Password = password,
                CountryId = command.CountryId,
                Phone_number = command.Phone_number
            };

            var response = await _userPersonRespository.CreateUserPerson(request) ?? throw new Exception();

            string baseUrl = _configuration.GetSection("EmailSettings:ValidationUrl").Value!;

            EmailRequest EmailRq = new EmailRequest();
            EmailRq.Para = command.Email;
            EmailRq.Asunto = "Confirmación de correo.";
            string variable = response.Guid;
            EmailRq.Contenido = $"<p>Para confirmar el correo y acceder a su cuenta haga click <a href='{baseUrl}{variable}'>aqui</a></p>";
            // Enviar correo electrónico de confirmación
            await _emailService.SendEmailAsync(EmailRq);

            return new Response<CreateUserPersonResponse>(response);
        }
    }
}

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
            EmailRq.Asunto = "MEJORA - Confirmación de correo.";
            string variable = response.Guid;
            // Contenido HTML con estilos inline para personalizar el color del h1 y la alineación
            EmailRq.Contenido = $@"
                                    <div style='width: 100%; height: 100%; display: flex; flex-direction: column; justify-content: center; align-items: center;'>
                                        <!-- Caja del título -->
                                        <div style='width: 100%; background-color: black; padding: 20px;'>
                                            <h1 style='color: white; text-align: left; margin: 0; margin-left: 20px;'>MEJORA</h1>
                                        </div>
                                        <!-- Caja del contenido -->
                                        <div style='width: 100%; background-color: white; color: black; border: 1px solid black; padding: 20px; box-sizing: border-box;'>
                                            <h2>Bienvenid@,</h2>
                                            <p>Para confirmar el correo y acceder a su cuenta haga click 
                                               <a href='{baseUrl}{variable}' style='color: #2196F3;'>aquí</a>.
                                            </p>
                                        </div>
                                    </div>";


            await _emailService.SendEmailAsync(EmailRq);

            return new Response<CreateUserPersonResponse>(response);
        }
    }
}

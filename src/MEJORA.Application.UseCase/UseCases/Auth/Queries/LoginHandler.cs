using MediatR;
using MEJORA.Application.Dtos.Auth.Request;
using MEJORA.Application.Dtos.Auth.Response;
using MEJORA.Application.Dtos.UserPerson.Request;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Authentication;
using MEJORA.Infrastructure.CrossCutting.Exceptions;

namespace MEJORA.Application.UseCase.UseCases.Auth.Queries
{
    public class LoginHandler : IRequestHandler<LoginQuery, Response<LoginResponse>>
    {
        private readonly IUserPersonRespository _userPersonRespository;
        private readonly IJwtGenerator _jwtGenerator;
        public LoginHandler(IUserPersonRespository userPersonRespository, IJwtGenerator jwtGenerator)
            => (_userPersonRespository, _jwtGenerator) = (userPersonRespository, jwtGenerator);

        public async Task<Response<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var requestMappig = new GetUserPersonByEmailRequest
            {
                Email = request.Email,
            };

            var userPerson = await _userPersonRespository.GetUserPersonByEmail(requestMappig) ?? throw new KeyNotFoundException("Usuario no existe en la base de datos."); ;

            if (userPerson is null || !BCrypt.Net.BCrypt.Verify(request.Password, userPerson.Password))
            {
                throw new BadRequestException("Incorrect credentials");
            }

            if(userPerson.State_Validation_Email == null || userPerson.State_Validation_Email == false)
            {
                throw new BadRequestException("The email has not been validated.");
            }

            var claims = new ClaimnsRequest
            {
                Email = userPerson.Email,
                Username = userPerson.Username
            };

            var loginResponse = new LoginResponse
            {
                Id= userPerson.Id,
                FirstName = userPerson.Firstname,
                LastName = userPerson.Lastname,
                Username = userPerson.Username,
                Email = userPerson.Email,
                Token = _jwtGenerator.GenerateToken(claims)
            };

            return new Response<LoginResponse>(loginResponse);
        }
    }
}

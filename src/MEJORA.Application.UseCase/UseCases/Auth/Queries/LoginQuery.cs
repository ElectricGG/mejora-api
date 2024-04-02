using MediatR;
using MEJORA.Application.Dtos.Auth.Response;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.Auth.Queries
{
    public class LoginQuery : IRequest<Response<LoginResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

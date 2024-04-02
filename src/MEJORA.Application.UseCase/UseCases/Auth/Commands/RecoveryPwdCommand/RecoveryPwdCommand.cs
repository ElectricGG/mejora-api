using MediatR;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.Auth.Commands.RecoveryPwdCommand
{
    public class RecoveryPwdCommand : IRequest<Response<bool>>
    {
        public string Email { get; set; }
    }
}

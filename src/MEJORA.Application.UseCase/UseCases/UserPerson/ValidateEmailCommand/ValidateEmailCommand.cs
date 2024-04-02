using MediatR;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.UserPerson.ValidateEmailCommand
{
    public class ValidateEmailCommand : IRequest<Response<bool>>
    {
        public string Identifier { get; set; }
    }
}

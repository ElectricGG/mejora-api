using MediatR;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.VideoQuestion.Commands
{
    public class RegisterResponseCommand : IRequest<Response<bool>>
    {
        public int UserPersonId { get; set; }
        public string Response { get; set; }
        public int VideoQuestionId { get; set; }
    }
}

using MediatR;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.VideoQuestion.Commands
{
    public class RegisterVideoQuestionCommand : IRequest<Response<bool>>
    {
        public int UserPersonId { get; set; }
        public int LessonVideoId { get; set; }
        public string Comment { get; set; }
        public int TimeQuestion { get; set; }
    }
}

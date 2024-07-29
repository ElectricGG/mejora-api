using MediatR;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.UserProgress.Commands
{
    public class UserProgressRegisterCommand : IRequest<Response<bool>>
    {
        public int LessonVideoId { get; set; }
        public int UserPersonId { get; set; }
        public int SecondsElapsed { get; set; }
    }
}

using MediatR;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.VideoUserCheck.Commands
{
    public class VideoUserCheckStateCommand : IRequest<Response<bool>>
    {
        public int LessonVideoId { get; set; }
        public int UserPersonId { get; set; }
        public int CheckedState { get; set; }
    }
}

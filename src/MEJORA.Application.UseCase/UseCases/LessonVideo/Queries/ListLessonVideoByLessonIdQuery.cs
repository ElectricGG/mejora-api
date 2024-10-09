using MediatR;
using MEJORA.Application.Dtos.LessonVideo.Response;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.LessonVideo.Queries
{
    public class ListLessonVideoByLessonIdQuery : IRequest<Response<ListLessonVideoByLessonIdResponse>>
    {
        public int LessonId { get; set; }
    }
}

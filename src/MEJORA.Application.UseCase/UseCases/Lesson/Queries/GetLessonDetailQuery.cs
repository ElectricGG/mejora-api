using MediatR;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Queries
{
    public class GetLessonDetailQuery : IRequest<Response<GetLessonDetailResponse>>
    {
        public int LessonId { get; set; }
    }
}

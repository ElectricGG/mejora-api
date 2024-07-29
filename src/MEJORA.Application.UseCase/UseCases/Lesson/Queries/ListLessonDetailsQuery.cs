using MediatR;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Queries
{
    public class ListLessonDetailsQuery : IRequest<Response<ListLessonsDetailsResponse>>
    {
        public int CourseId { get; set; }
    }
}

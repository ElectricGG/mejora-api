using MediatR;
using MEJORA.Application.Dtos.CourseLesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.CourseLesson.Queries
{
    public class ListLessonByCourseIdQuery : IRequest<Response<ListLessonByCourseResponse>>
    {
        public int CourseId { get; set; }
    }
}

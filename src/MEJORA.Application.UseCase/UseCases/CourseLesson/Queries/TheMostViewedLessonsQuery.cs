using MediatR;
using MEJORA.Application.Dtos.CourseLesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.CourseLesson.Queries
{
    public class TheMostViewedLessonsQuery : IRequest<Response<CourseLessonMostViewResponse>>
    {
    }
}

using MediatR;
using MEJORA.Application.Dtos.CourseLesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.CourseLesson.Queries
{
    public class TheMostViewedLessonsHandler : IRequestHandler<TheMostViewedLessonsQuery, Response<CourseLessonMostViewResponse>>
    {
        private readonly ICourseLessonRepository _repository;
        public TheMostViewedLessonsHandler(ICourseLessonRepository repository)
            => _repository = repository;
        public async Task<Response<CourseLessonMostViewResponse>> Handle(TheMostViewedLessonsQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.ListCourseLessonMostView();
            return new Response<CourseLessonMostViewResponse>(response);
        }
    }
}

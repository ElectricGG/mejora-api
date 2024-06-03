using MediatR;
using MEJORA.Application.Dtos.CourseLesson.Request;
using MEJORA.Application.Dtos.CourseLesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.CourseLesson.Queries
{
    public class ListByNameHandler : IRequestHandler<ListByNameQuery, Response<CourseLessonListResponse>>
    {
        private readonly ICourseLessonRepository _repository;
        public ListByNameHandler(ICourseLessonRepository repository)
            => _repository = repository;
        public async Task<Response<CourseLessonListResponse>> Handle(ListByNameQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.CourseLessonListByName(new CourseLessonListRequest { Name = request.Name});
            return new Response<CourseLessonListResponse>(response);
        }
    }
}

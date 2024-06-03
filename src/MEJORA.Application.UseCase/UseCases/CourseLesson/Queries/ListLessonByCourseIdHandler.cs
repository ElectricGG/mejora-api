using MediatR;
using MEJORA.Application.Dtos.CourseLesson.Request;
using MEJORA.Application.Dtos.CourseLesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.CourseLesson.Queries
{
    public class ListLessonByCourseIdHandler : IRequestHandler<ListLessonByCourseIdQuery, Response<ListLessonByCourseResponse>>
    {
        private readonly ICourseLessonRepository _repository;
        public ListLessonByCourseIdHandler(ICourseLessonRepository repository)
            => _repository = repository;

        public async Task<Response<ListLessonByCourseResponse>> Handle(ListLessonByCourseIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.ListLessonByCourseId(new ListLessonByCourseRequest { CourseId = request.CourseId });
            return new Response<ListLessonByCourseResponse>(response);
        }
    }
}

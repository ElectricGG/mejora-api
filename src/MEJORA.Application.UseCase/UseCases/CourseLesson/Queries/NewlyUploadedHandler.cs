using MediatR;
using MEJORA.Application.Dtos.CourseLesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.CourseLesson.Queries
{
    public class NewlyUploadedHandler : IRequestHandler<NewlyUploadedQuery, Response<CourseLessonNewlyUploadedResponse>>
    {
        private readonly ICourseLessonRepository _repository;
        public NewlyUploadedHandler(ICourseLessonRepository repository)
            => _repository = repository;

        public async Task<Response<CourseLessonNewlyUploadedResponse>> Handle(NewlyUploadedQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.CourseLessonNewlyUploaded();
            return new Response<CourseLessonNewlyUploadedResponse>(response);
        }
    }
}

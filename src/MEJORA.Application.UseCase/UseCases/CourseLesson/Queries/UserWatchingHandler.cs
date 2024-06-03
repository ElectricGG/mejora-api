using MediatR;
using MEJORA.Application.Dtos.CourseLesson.Request;
using MEJORA.Application.Dtos.CourseLesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.CourseLesson.Queries
{
    public class UserWatchingHandler : IRequestHandler<UserWatchingQuery, Response<CourseLessonUserWatchingResponse>>
    {
        private readonly ICourseLessonRepository _repository;
        public UserWatchingHandler(ICourseLessonRepository repository)
            => _repository = repository;

        public async Task<Response<CourseLessonUserWatchingResponse>> Handle(UserWatchingQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.CourseLessonUserWatching(new CourseLessonUserWatchingRequest { UserPersonId = request.UserPersonId});
            return new Response<CourseLessonUserWatchingResponse>(response);
        }
    }
}

using MediatR;
using MEJORA.Application.Dtos.LessonVideo.Request;
using MEJORA.Application.Dtos.LessonVideo.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.LessonVideo.Queries
{
    public class ListLessonsVideoHandler : IRequestHandler<ListLessonsVideoQuery, Response<ListLessonsVideoResponse>>
    {
        private readonly ILessonVideoRepository _repository;
        public ListLessonsVideoHandler(ILessonVideoRepository repository)
            => _repository = repository;

        public async Task<Response<ListLessonsVideoResponse>> Handle(ListLessonsVideoQuery request, CancellationToken cancellationToken)
        {
            var requestMap = new ListLessonsVideoRequest { LessonId = request.LessonId, UserPersonId = request.UserPersonId };
            var response = await _repository.ListLessonByCourseId(requestMap);
            return new Response<ListLessonsVideoResponse>(response);
        }
    }
}

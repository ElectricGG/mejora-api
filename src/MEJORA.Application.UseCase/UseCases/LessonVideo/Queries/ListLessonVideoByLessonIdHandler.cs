using MediatR;
using MEJORA.Application.Dtos.LessonVideo.Request;
using MEJORA.Application.Dtos.LessonVideo.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.LessonVideo.Queries
{
    public class ListLessonVideoByLessonIdHandler : IRequestHandler<ListLessonVideoByLessonIdQuery, Response<ListLessonVideoByLessonIdResponse>>
    {
        private readonly ILessonVideoRepository _repository;
        public ListLessonVideoByLessonIdHandler(ILessonVideoRepository repository)
            => _repository = repository;
        public async Task<Response<ListLessonVideoByLessonIdResponse>> Handle(ListLessonVideoByLessonIdQuery request, CancellationToken cancellationToken)
        {
            var requestMap = new ListLessonVideoByLessonIdRequest { LessonId = request.LessonId };
            var response = await _repository.ListLessonVideoByLessonId(requestMap);
            return new Response<ListLessonVideoByLessonIdResponse>(response);
        }
    }
}

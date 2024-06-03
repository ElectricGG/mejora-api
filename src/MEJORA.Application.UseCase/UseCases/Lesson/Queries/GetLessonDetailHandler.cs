using MediatR;
using MEJORA.Application.Dtos.Lesson.Request;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Queries
{
    public class GetLessonDetailHandler : IRequestHandler<GetLessonDetailQuery, Response<GetLessonDetailResponse>>
    {
        private readonly ILessonRepository _repository;
        public GetLessonDetailHandler(ILessonRepository repository)
            => _repository = repository;

        public async Task<Response<GetLessonDetailResponse>> Handle(GetLessonDetailQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetLessonDetail(new GetLessonDetailRequest { LessonId = request.LessonId });
            return new Response<GetLessonDetailResponse>(response);
        }
    }
}

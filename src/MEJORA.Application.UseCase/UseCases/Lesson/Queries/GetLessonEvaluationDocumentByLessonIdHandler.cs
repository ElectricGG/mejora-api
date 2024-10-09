using MediatR;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Queries
{
    public class GetLessonEvaluationDocumentByLessonIdHandler : IRequestHandler<GetLessonEvaluationDocumentByLessonIdQuery, Response<GetLessonEvaluationDocumentByLessonIdResponse>>
    {
        private readonly ILessonRepository _repository;
        public GetLessonEvaluationDocumentByLessonIdHandler(ILessonRepository repository)
            => _repository = repository;

        public async Task<Response<GetLessonEvaluationDocumentByLessonIdResponse>> Handle(GetLessonEvaluationDocumentByLessonIdQuery query, CancellationToken cancellationToken)
        {
            var response = await _repository.getLessonEvaluationDocumentByLessonId(query.LessonId);
            return new Response<GetLessonEvaluationDocumentByLessonIdResponse>(response);
        }
    }
}

using MediatR;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Queries
{
    public class GetSatisfactionSurveyByLessonIdHandler : IRequestHandler<GetSatisfactionSurveyByLessonIdQuery, Response<GetSatisfactionSurveyByLessonIdResponse>>
    {
        private readonly ILessonRepository _repository;
        public GetSatisfactionSurveyByLessonIdHandler(ILessonRepository repository)
            => _repository = repository;
        public async Task<Response<GetSatisfactionSurveyByLessonIdResponse>> Handle(GetSatisfactionSurveyByLessonIdQuery query, CancellationToken cancellationToken)
        {
            var response = await _repository.GetSatisfactionSurveyByLessonId(query.LessonId);
            return new Response<GetSatisfactionSurveyByLessonIdResponse>(response);
        }
    }
}

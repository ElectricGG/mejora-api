using MediatR;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Queries
{
    public class ListEvaluationResolveHandler : IRequestHandler<ListEvaluationResolveQuery, Response<ListEvaluationResolveResponse>>
    {
        private readonly ILessonRepository _lessonRepository;
        public ListEvaluationResolveHandler(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }
        public async Task<Response<ListEvaluationResolveResponse>> Handle(ListEvaluationResolveQuery request, CancellationToken cancellationToken)
        {
            var response = await _lessonRepository.ListEvaluationResolves();
            return new Response<ListEvaluationResolveResponse>(response);
        }
    }
}

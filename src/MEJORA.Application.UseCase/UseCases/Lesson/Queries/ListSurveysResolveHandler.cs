using MediatR;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Queries
{
    public class ListSurveysResolveHandler : IRequestHandler<ListSurveysResolveQuery, Response<ListSurveysResolveResponse>>
    {
        private readonly ILessonRepository _lessonRepository;
        public ListSurveysResolveHandler(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }
        public async Task<Response<ListSurveysResolveResponse>> Handle(ListSurveysResolveQuery request, CancellationToken cancellationToken)
        {
            var response = await _lessonRepository.ListSurveysResolves();
            return new Response<ListSurveysResolveResponse>(response);
        }
    }
}

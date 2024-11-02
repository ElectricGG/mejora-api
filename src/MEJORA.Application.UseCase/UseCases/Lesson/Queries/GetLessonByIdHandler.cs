using MediatR;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Queries
{
    public class GetLessonByIdHandler : IRequestHandler<GetLessonByIdQuery, Response<GetLessonByIdResponse>>
    {
        private readonly ILessonRepository _repository;
        public GetLessonByIdHandler(ILessonRepository repository)
            => _repository = repository;
        public async Task<Response<GetLessonByIdResponse>> Handle(GetLessonByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetLessonById(request.Id);
            return new Response<GetLessonByIdResponse>(response);
        }
    }
}

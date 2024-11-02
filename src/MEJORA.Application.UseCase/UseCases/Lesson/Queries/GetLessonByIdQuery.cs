using MediatR;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Queries
{
    public class GetLessonByIdQuery : IRequest<Response<GetLessonByIdResponse>>
    {
        public int Id { get; set; }
    }
}

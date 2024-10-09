using MediatR;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using Microsoft.AspNetCore.Http;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Commands
{
    public class CreateEvaluationResolveCommand : IRequest<Response<CreateEvaluationResolveResponse>>
    {
        public int UserResolveId { get; set; }
        public int LessonId { get; set; }
        public int LessonEvaluationDocumentId { get; set; }
        public IFormFile file { get; set; }
    }
}

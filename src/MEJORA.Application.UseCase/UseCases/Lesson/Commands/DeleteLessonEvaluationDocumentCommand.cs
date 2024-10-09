using MediatR;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Commands
{
    public class DeleteLessonEvaluationDocumentCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
    }
}

using MediatR;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Commands
{
    public class DeleteSatisfactionSurveyCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
    }
}

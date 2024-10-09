using MediatR;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.LessonVideo.Commands
{
    public class DeleteLessonVideoCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
    }
}

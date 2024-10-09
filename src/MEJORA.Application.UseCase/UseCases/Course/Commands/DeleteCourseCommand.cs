using MediatR;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.Course.Commands
{
    public class DeleteCourseCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public int CourseProjectId { get; set; }
    }
}

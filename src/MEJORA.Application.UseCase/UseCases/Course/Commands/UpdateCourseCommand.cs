using MediatR;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.Course.Commands
{
    public class UpdateCourseCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public int CourseProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

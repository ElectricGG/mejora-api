using MediatR;
using MEJORA.Application.Dtos.Course.Response;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.Course.Commands
{
    public class CreateCourseCommand : IRequest<Response<CreateCourseResponse>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserCreatorId { get; set; }
    }
}

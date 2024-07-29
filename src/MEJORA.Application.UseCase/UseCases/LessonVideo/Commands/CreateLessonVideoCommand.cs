using MediatR;
using MEJORA.Application.Dtos.LessonVideo.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using Microsoft.AspNetCore.Http;

namespace MEJORA.Application.UseCase.UseCases.LessonVideo.Commands
{
    public class CreateLessonVideoCommand : IRequest<Response<CreateLessonVideoResponse>>
    {
        public int LessonId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserCreatorId { get; set; }
        public int? PlayOrder { get; set; }
        public int? CourseProjectId { get; set; }
        public IFormFile videoFile { get; set; }
    }
}

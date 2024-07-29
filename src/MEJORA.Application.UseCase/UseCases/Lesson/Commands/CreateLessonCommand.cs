using MediatR;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using Microsoft.AspNetCore.Http;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Commands
{
    public class CreateLessonCommand : IRequest<Response<CreateLessonResponse>>
    {
        public int CourseProjectId { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserCreatorId { get; set; }
        public IFormFile? PreviousImage { get; set; }
        public int? LessonOrder { get; set; }
        public string? Objectives { get; set; }
        public string? Bibliography { get; set; }
        public string? CvInstructor { get; set; }
        public string? InstructorName { get; set; }
        public string? InstructorProfession { get; set; }
    }
}

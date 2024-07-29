using MEJORA.Application.Dtos.LessonVideo.Request;
using Microsoft.AspNetCore.Http;

namespace MEJORA.Application.Dtos.Lesson.Request
{
    public class CreateLessonRequest
    {
        public int CourseProjectId { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserCreatorId { get; set; }
        public IFormFile PreviousImage { get; set; }
        public int? LessonOrder { get; set; }
        public string? Objectives { get; set; }
        public string? Bibliography { get; set; }
        public string? CvInstructor { get; set; }
        public string? IndexLesson { get; set; }
        public string? InstructorName { get; set; }
        public string? InstructorProfession { get; set; }
    }
}

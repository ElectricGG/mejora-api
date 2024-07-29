using Microsoft.AspNetCore.Http;

namespace MEJORA.Application.Dtos.LessonVideo.Request
{
    public class CreateLessonVideoRequest
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

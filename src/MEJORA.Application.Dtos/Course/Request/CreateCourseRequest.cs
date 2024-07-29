using MEJORA.Application.Dtos.Lesson.Request;

namespace MEJORA.Application.Dtos.Course.Request
{
    public class CreateCourseRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? DurationMinutes { get; set; } 
        public int? CourseLevelId { get; set; } 
        public int UserCreatorId { get; set; }
        public string? PreviousImage { get; set; }
        public List<CreateLessonRequest> createLessonRequest { get; set; }

    }

    
}

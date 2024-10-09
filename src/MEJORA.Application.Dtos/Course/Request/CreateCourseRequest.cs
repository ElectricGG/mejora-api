using MEJORA.Application.Dtos.Lesson.Request;

namespace MEJORA.Application.Dtos.Course.Request
{
    public class CreateCourseRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserCreatorId { get; set; }
    }

    
}

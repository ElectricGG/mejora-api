namespace MEJORA.Application.Dtos.CourseLesson.Request
{
    public class CreateCourseLessonRequest
    {
        public int CourseId { get; set; }
        public int LessonId { get; set; }
        public int UserCreatorId { get; set; }
    }

}

namespace MEJORA.Application.Dtos.CourseLesson.Response
{
    public class CourseLessonMostViewResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MinutesElapsed { get; set; }
        public string? PreviousImage { get; set; }
    }
}

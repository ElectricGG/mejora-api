namespace MEJORA.Application.Dtos.Lesson.Response
{
    public class ListEvaluationResolveResponse
    {
        public string CourseName { get; set; }
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public int EvaluationResolveId { get; set; }
        public string EvaluationResolveFileName { get; set; }
        public string CreatedAt { get; set; }
        public string EvaluatedUser { get; set; }
        public string Url { get; set; }
    }
}

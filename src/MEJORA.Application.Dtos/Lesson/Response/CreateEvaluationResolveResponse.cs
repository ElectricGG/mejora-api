namespace MEJORA.Application.Dtos.Lesson.Response
{
    public class CreateEvaluationResolveResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int UserResolveId { get; set; }
        public int LessonId { get; set; }
        public int LessonEvaluationDocumentId { get; set; }
    }
}

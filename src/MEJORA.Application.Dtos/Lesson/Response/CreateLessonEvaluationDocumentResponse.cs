namespace MEJORA.Application.Dtos.Lesson.Response
{
    public class CreateLessonEvaluationDocumentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int UserPersonId { get; set; }
    }
}

namespace MEJORA.Application.Dtos.VideoQuestion.Request
{
    public class RegisterVideoQuestionRequest
    {
        public int UserPersonId { get; set; }
        public int LessonVideoId { get; set; }
        public string Comment { get; set; }
        public int TimeQuestion { get; set; }
    }
}

namespace MEJORA.Application.Dtos.VideoQuestion.Response
{
    public class ListVideoQuestionResponse
    {
        public string CourseName { get; set; }
        public string LessonName { get; set; }
        public int LessonVideoId { get; set; }
        public string LessonVideoName { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public int Id { get; set; }
        public string Response {  get; set; }
        public string FechaRegistro { get; set; }
        public string TimeQuestion { get; set; }
    }
}

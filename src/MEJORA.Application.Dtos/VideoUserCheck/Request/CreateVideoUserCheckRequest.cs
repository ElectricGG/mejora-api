namespace MEJORA.Application.Dtos.VideoUserCheck.Request
{
    public class CreateVideoUserCheckRequest
    {
        public int UserPersonId { get; set; }
        public int LessonVideoId { get; set; }
        public int CheckState { get; set; }
    }
}

namespace MEJORA.Application.Dtos.VideoUserCheck.Request
{
    public class VideoUserCheckStateRequest
    {
        public int LessonVideoId { get; set; }
        public int UserPersonId { get; set; }
        public int CheckedState { get; set; }
    }
}

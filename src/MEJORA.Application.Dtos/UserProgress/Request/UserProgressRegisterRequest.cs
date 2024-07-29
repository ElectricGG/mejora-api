namespace MEJORA.Application.Dtos.UserProgress.Request
{
    public class UserProgressRegisterRequest
    {
        public int LessonVideoId { get; set; }
        public int UserPersonId { get; set; }
        public int SecondsElapsed { get; set; }
    }
}

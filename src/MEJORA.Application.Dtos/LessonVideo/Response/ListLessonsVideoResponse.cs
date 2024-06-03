namespace MEJORA.Application.Dtos.LessonVideo.Response
{
    public class ListLessonsVideoResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? HtmlContent { get; set; }
        public string? FormattedDuration { get; set; }
        public int PlayOrder { get; set; }
        public int HasBeenPlayed { get; set; }
    }
}

namespace MEJORA.Application.Dtos.Lesson.Response
{
    public class ListLessonsDetailsResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PreviousImage { get; set; }
        public string? NameLessonOrder { get; set; }
        public List<ListLessonVideoDetailsResponse>? listLessonVideoDetailsResponses { get; set; }
    }

    public class ListLessonVideoDetailsResponse
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FormattedDuration { get; set; }
        public int PlayOrder { get; set; }
    }
}

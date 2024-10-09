namespace MEJORA.Application.Dtos.Course.Response
{
    public class ListCoursesAdminResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public string CreatedAt { get; set; }
        public string UserCreator {  get; set; }
        public int? CourseProjectId { get; set; }
    }
}

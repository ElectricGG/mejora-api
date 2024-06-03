using MEJORA.Application.Dtos.Course.Response;

namespace MEJORA.Application.Interface
{
    public interface ICourseRepository
    {
        Task<List<ListCoursesResponse>> ListCourses();
    }
}

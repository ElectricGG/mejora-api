using MEJORA.Application.Dtos.LessonVideo.Request;
using MEJORA.Application.Dtos.LessonVideo.Response;

namespace MEJORA.Application.Interface
{
    public interface ILessonVideoRepository
    {
        Task<List<ListLessonsVideoResponse>> ListLessonByCourseId(ListLessonsVideoRequest request);
    }
}

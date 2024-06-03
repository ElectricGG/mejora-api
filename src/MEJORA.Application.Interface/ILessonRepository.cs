using MEJORA.Application.Dtos.Lesson.Request;
using MEJORA.Application.Dtos.Lesson.Response;

namespace MEJORA.Application.Interface
{
    public interface ILessonRepository
    {
        Task<GetLessonDetailResponse> GetLessonDetail(GetLessonDetailRequest request);
    }
}

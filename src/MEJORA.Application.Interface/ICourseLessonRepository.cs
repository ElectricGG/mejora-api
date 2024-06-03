using MEJORA.Application.Dtos.CourseLesson.Request;
using MEJORA.Application.Dtos.CourseLesson.Response;

namespace MEJORA.Application.Interface
{
    public interface ICourseLessonRepository
    {
        Task<List<CourseLessonMostViewResponse>> ListCourseLessonMostView();
        Task<List<CourseLessonNewlyUploadedResponse>> CourseLessonNewlyUploaded();
        Task<List<CourseLessonUserWatchingResponse>> CourseLessonUserWatching(CourseLessonUserWatchingRequest request);
        Task<List<CourseLessonListResponse>> CourseLessonListByName(CourseLessonListRequest request);
        Task<List<ListLessonByCourseResponse>> ListLessonByCourseId(ListLessonByCourseRequest request);
    }
}

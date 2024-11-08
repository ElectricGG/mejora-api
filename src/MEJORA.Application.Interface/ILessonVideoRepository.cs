﻿using MEJORA.Application.Dtos.LessonVideo.Request;
using MEJORA.Application.Dtos.LessonVideo.Response;

namespace MEJORA.Application.Interface
{
    public interface ILessonVideoRepository
    {
        Task<List<ListLessonsVideoResponse>> ListLessonByCourseId(ListLessonsVideoRequest request);
        Task<CreateLessonVideoResponse> CreateLessonVideo(CreateLessonVideoRequest request);
        Task<bool> DeleteLessonVideo(DeleteLessonVideoRequest request);
        Task<List<ListLessonVideoByLessonIdResponse>> ListLessonVideoByLessonId(ListLessonVideoByLessonIdRequest request);
    }
}

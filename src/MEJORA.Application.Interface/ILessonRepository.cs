using MEJORA.Application.Dtos.Lesson.Request;
using MEJORA.Application.Dtos.Lesson.Response;

namespace MEJORA.Application.Interface
{
    public interface ILessonRepository
    {
        Task<GetLessonDetailResponse> GetLessonDetail(GetLessonDetailRequest request);
        Task<CreateLessonResponse> CreateLesson(CreateLessonRequest request);
        Task<List<ListLessonsDetailsResponse>> ListLessonsDetails(ListLessonsDetailsRequest request);
        Task<bool> UpdateLesson(UpdateLessonRequest request);
        Task<bool> DeleteLesson(DeleteLessonRequest request);
        Task<CreateLessonEvaluationDocumentResponse> CreateLessonEvaluationDocument(CreateLessonEvaluationDocumentRequest request);
        Task<GetLessonEvaluationDocumentByLessonIdResponse> getLessonEvaluationDocumentByLessonId(int LessonId);
        Task<bool> DeleteLessonEvaluationDocumentn(int id);
        Task<CreateEvaluationResolveResponse> CreateEvaluationResolve(CreateEvaluationResolveRequest request);
        Task<List<ListEvaluationResolveResponse>> ListEvaluationResolves();
        Task <CreateSatisfactionSurveyResponse> CreateSatisfactionSurvey(CreateSatisfactionSurveyRequest request);
        Task<GetSatisfactionSurveyByLessonIdResponse> GetSatisfactionSurveyByLessonId(int LessonId);
        Task<bool> DeleteSatisfactionSurvey(int id);
        Task<List<ListSurveysResolveResponse>> ListSurveysResolves();
        Task<CreateSatisfactionSurveyResolveResponse> CreateSatisfactionSurveyResolve(CreateSatisfactionSurveyResolveRequest request);
        Task<GetLessonByIdResponse> GetLessonById(int id);

    }
}

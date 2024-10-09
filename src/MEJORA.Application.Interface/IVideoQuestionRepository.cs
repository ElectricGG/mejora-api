using MEJORA.Application.Dtos.VideoQuestion.Request;
using MEJORA.Application.Dtos.VideoQuestion.Response;

namespace MEJORA.Application.Interface
{
    public interface IVideoQuestionRepository
    {
        Task<bool> RegisterVideoQuestion(RegisterVideoQuestionRequest request);
        Task<List<ListVideoQuestionResponse>> ListQuestionVideo();
        Task<bool> RegisterResponse(RegisterResponseRequest request);
    }
}

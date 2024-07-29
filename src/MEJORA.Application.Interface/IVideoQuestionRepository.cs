using MEJORA.Application.Dtos.VideoQuestion.Request;

namespace MEJORA.Application.Interface
{
    public interface IVideoQuestionRepository
    {
        Task<bool> RegisterVideoQuestion(RegisterVideoQuestionRequest request);

    }
}

using MEJORA.Application.Dtos.VideoUserCheck.Request;

namespace MEJORA.Application.Interface
{
    public interface IVideoUserCheckRepository
    {
        Task<bool> VideoUserCheckState(VideoUserCheckStateRequest request);
    }
}

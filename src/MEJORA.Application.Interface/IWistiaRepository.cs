using MEJORA.Application.Dtos.Wistia.Request;
using MEJORA.Application.Dtos.Wistia.Response;

namespace MEJORA.Application.Interface
{
    public interface IWistiaRepository
    {
        Task<UploadMediaResponse> UploadMedia(UploadMediaRequest request);
    }
}

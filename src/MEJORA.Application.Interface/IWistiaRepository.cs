using MEJORA.Application.Dtos.Course.Request;
using MEJORA.Application.Dtos.Course.Response;
using MEJORA.Application.Dtos.Wistia.Request;
using MEJORA.Application.Dtos.Wistia.Response;

namespace MEJORA.Application.Interface
{
    public interface IWistiaRepository
    {
        Task<UploadMediaResponse> UploadMedia(UploadMediaRequest request);
        Task<CreateProjectResponse> CreateProject(CreateProjectRequest request);
        Task<UpdateProjectResponse> UpdateProject(UpdateProjectRequest request);
        Task<DeleteProjectResponse> DeleteProject(DeleteProjectRequest request);
        Task<bool> DeleteMedia(string hashed_id);
        Task<bool> UpdateMedia(string hashed_id, string newName);
    }
}

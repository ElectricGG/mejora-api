using Microsoft.AspNetCore.Http;

namespace MEJORA.Application.Interface
{
    public interface IAzureStorage
    {
        Task<string> SaveFile(string container, IFormFile file, string codeGuid);
        Task<string> EditFile(string container, IFormFile file, string route, string codeGuid);
        Task RemoveFile(string route, string container);
    }
}

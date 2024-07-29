using Microsoft.AspNetCore.Http;

namespace MEJORA.Application.Dtos.Wistia.Request
{
    public class UploadMediaRequest
    {
        public IFormFile FilePath { get; set; }
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

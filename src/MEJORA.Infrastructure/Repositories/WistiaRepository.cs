using MEJORA.Application.Dtos.Wistia.Request;
using MEJORA.Application.Dtos.Wistia.Response;
using MEJORA.Application.Interface;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Net.Http.Headers;
using Newtonsoft.Json; // Asegúrate de tener esta referencia instalada en tu proyecto


namespace MEJORA.Infrastructure.Repositories
{
    public class WistiaRepository : IWistiaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public WistiaRepository(IConfiguration configuration, HttpClient httpClient) 
        { 
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<UploadMediaResponse> UploadMedia(UploadMediaRequest requestDto)
        {
            var responseDto = new UploadMediaResponse();

            try
            {
                var baseUrl = _configuration["WistiaIntegration:UploadMedia"]!;
                var token = _configuration["WistiaIntegration:AccessToken"]!;

                using var content = new MultipartFormDataContent();

                // Add the file content
                if (requestDto.FilePath != null)
                {
                    var fileContent = new StreamContent(requestDto.FilePath.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(requestDto.FilePath.ContentType);
                    content.Add(fileContent, "file", requestDto.FilePath.FileName);
                }

                // Add the other form data
                if (!string.IsNullOrEmpty(requestDto.ProjectId))
                {
                    content.Add(new StringContent(requestDto.ProjectId), "project_id");
                }

                if (!string.IsNullOrEmpty(requestDto.Name))
                {
                    content.Add(new StringContent(requestDto.Name), "name");
                }

                if (!string.IsNullOrEmpty(requestDto.Description))
                {
                    content.Add(new StringContent(requestDto.Description), "description");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Replace "your_api_url" with the actual API URL
                var response = await _httpClient.PostAsync(baseUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseDto = JsonConvert.DeserializeObject<UploadMediaResponse>(responseContent);
                }

                return responseDto;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}

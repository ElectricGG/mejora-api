using Dapper;
using MEJORA.Application.Dtos.Course.Request;
using MEJORA.Application.Dtos.Course.Response;
using MEJORA.Application.Dtos.Wistia.Request;
using MEJORA.Application.Dtos.Wistia.Response;
using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data; // Asegúrate de tener esta referencia instalada en tu proyecto
using System.Net.Http.Headers;
using System.Text;


namespace MEJORA.Infrastructure.Repositories
{
    public class WistiaRepository : IWistiaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ApplicationDdContext _context;

        public WistiaRepository(IConfiguration configuration, HttpClient httpClient, ApplicationDdContext context) 
        { 
            _configuration = configuration;
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<CreateProjectResponse> CreateProject(CreateProjectRequest request)
        {
            var responseDto = new CreateProjectResponse();

            try
            {
                var baseUrl = _configuration["WistiaIntegration:CreateProject"]!;
                var token = _configuration["WistiaIntegration:AccessToken"]!;

                using var content = new MultipartFormDataContent();

                if (!string.IsNullOrEmpty(request.Name))
                {
                    content.Add(new StringContent(request.Name), "name");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsync(baseUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseDto = JsonConvert.DeserializeObject<CreateProjectResponse>(responseContent);
                }

                return responseDto;
            }
            catch (Exception ex)
            {
                using var connection = _context.CreateConnection;
                string procedure = "spCreateLogExceptions";

                var parametros = new DynamicParameters();
                parametros.Add("@result", ex.Message + " ___ " + ex.InnerException + " ___ " + ex.StackTrace);

                var affectedRows = await connection.ExecuteAsync(
                    procedure,
                    param: parametros,
                    commandType: CommandType.StoredProcedure
                );

                throw ex;
            }
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

        public async Task<UpdateProjectResponse> UpdateProject(UpdateProjectRequest request)
        {
            var responseDto = new UpdateProjectResponse();

            try
            {
                var baseUrl = _configuration["WistiaIntegration:UpdateOrDeleteProject"]! + request.Id;
                var token = _configuration["WistiaIntegration:AccessToken"]!;

                // Crear un objeto anónimo para el cuerpo de la solicitud
                var body = new
                {
                    name = request.Name,
                    description = request.Description
                };

                // Serializar el cuerpo a JSON
                var jsonContent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                // Configurar la cabecera de autorización
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Enviar la solicitud PUT a la API con el cuerpo JSON
                var response = await _httpClient.PutAsync(baseUrl, jsonContent);

                // Verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Leer y deserializar el contenido de la respuesta
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseDto = JsonConvert.DeserializeObject<UpdateProjectResponse>(responseContent);
                }

                // Devolver el resultado
                return responseDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DeleteProjectResponse> DeleteProject(DeleteProjectRequest request)
        {
            var responseDto = new DeleteProjectResponse();

            try
            {
                var baseUrl = _configuration["WistiaIntegration:UpdateOrDeleteProject"]! + request.Id;
                var token = _configuration["WistiaIntegration:AccessToken"]!;

                // Configurar la cabecera de autorización
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Enviar la solicitud PUT a la API con el cuerpo JSON
                var response = await _httpClient.DeleteAsync(baseUrl);

                // Verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Leer y deserializar el contenido de la respuesta
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseDto = JsonConvert.DeserializeObject<DeleteProjectResponse>(responseContent);
                }

                // Devolver el resultado
                return responseDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteMedia(string hashed_id)
        {
            var responseDto = true;

            try
            {
                var baseUrl = _configuration["WistiaIntegration:UpdateOrDeleteMedia"]! + "/"+hashed_id+".json";
                var token = _configuration["WistiaIntegration:AccessToken"]!;

                // Configurar la cabecera de autorización
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Enviar la solicitud PUT a la API con el cuerpo JSON
                var response = await _httpClient.DeleteAsync(baseUrl);

                // Verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    responseDto = true;
                }
                else
                {
                    responseDto = false;
                }

                // Devolver el resultado
                return responseDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateMedia(string hashed_id, string newName)
        {
            var responseDto = true;

            try
            {
                var baseUrl = _configuration["WistiaIntegration:UpdateOrDeleteMedia"]! + "/" + hashed_id + ".json";
                var token = _configuration["WistiaIntegration:AccessToken"]!;

                // Crear un objeto anónimo para el cuerpo de la solicitud
                var body = new
                {
                    name = newName
                };

                // Serializar el cuerpo a JSON
                var jsonContent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                // Configurar la cabecera de autorización
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Enviar la solicitud PUT a la API con el cuerpo JSON
                var response = await _httpClient.PutAsync(baseUrl, jsonContent);

                // Verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    responseDto = true;
                }
                else
                {
                    responseDto = false;
                }

                // Devolver el resultado
                return responseDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

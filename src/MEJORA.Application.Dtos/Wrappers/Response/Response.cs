using System.Text.Json.Serialization;
using System.Text.Json;

namespace MEJORA.Application.Dtos.Wrappers.Response
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = "";

        public List<string> Errors { get; set; } = new List<string>();
        public object? Data { get; set; } = new object();

        public Response() { }

        public Response(T data, string message = "")
        {
            Data = data;
            Message = message;
            Succeeded = true;
        }
        public Response(List<T> dataList, string message = "")
        {
            Data = dataList;
            Message = message;
            Succeeded = true;
        }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
                //  IgnoreNullValues = true
            });
        }
    }
}

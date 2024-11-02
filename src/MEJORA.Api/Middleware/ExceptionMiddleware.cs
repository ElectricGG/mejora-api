using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Infrastructure.CrossCutting.Exceptions;
using System.Net;

namespace bvs.cotizador.auth.WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
                if (context.Response.StatusCode == 401)
                {
                    var responseModel = new Response<string> { Succeeded = false, Message = "Usuario no autenticado" };
                    await context.Response.WriteAsync(responseModel.ToString());
                }
            }
            catch (Exception ex)
            {
                await AsyncExceptionHandler(context, ex);
            }
        }

        private static async Task AsyncExceptionHandler(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var responseModel = new Response<string> { Succeeded = false, Message = ex.Message };

            switch (ex)
            {
                case BadRequestException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                //case ValidationException e:
                //    response.StatusCode = (int)HttpStatusCode.BadRequest;
                //    responseModel.Errors = e.Errors;
                //    break;
                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                //case ConflictValueException:
                //    response.StatusCode = (int)HttpStatusCode.Conflict;
                //    break;
                //case PermissionException:
                //    response.StatusCode = (int)HttpStatusCode.Forbidden;
                //    break;
                //case UnauthorizedExcepction:
                //    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                //    break;
                default:
                    response.StatusCode = (int)(HttpStatusCode.InternalServerError);
                    break;
            }

            await response.WriteAsync(responseModel.ToString());
        }

    }
}

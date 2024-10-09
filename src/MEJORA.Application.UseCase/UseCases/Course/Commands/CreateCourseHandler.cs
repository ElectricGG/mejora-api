using MediatR;
using MEJORA.Application.Dtos.Course.Request;
using MEJORA.Application.Dtos.Course.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.Course.Commands
{
    public class CreateCourseHandler : IRequestHandler<CreateCourseCommand, Response<CreateCourseResponse>>
    {
        private readonly ICourseRepository _courseRepository;

        public CreateCourseHandler(ICourseRepository courseRepository)
            => _courseRepository = courseRepository;

        public async Task<Response<CreateCourseResponse>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var createResponse = new CreateCourseResponse();
            var response = new Response<CreateCourseResponse>(createResponse, "Curso registrado correctamente.");

            var mapDto = new CreateCourseRequest()
            {
                Name = request.Name,
                Description = request.Description,
                UserCreatorId = request.UserCreatorId,
            };

            createResponse = await _courseRepository.CreateCourses(mapDto);

            if(createResponse == null)
            {
                response.Succeeded = false;
                response.Message = "No se pudo registrar el curso. Contacte con soporte.";

                return response;
            }

            response.Data = createResponse;

            return response;
        }
    }
}

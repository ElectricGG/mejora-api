using MediatR;
using MEJORA.Application.Dtos.Course.Request;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.Course.Commands
{
    public class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand, Response<bool>>
    {
        private readonly ICourseRepository _courseRepository;

        public UpdateCourseHandler(ICourseRepository courseRepository)
            => _courseRepository = courseRepository;

        public async Task<Response<bool>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>(true, "Curso actualizado correctamente.");

            UpdateCourseRequest mapDto = new()
            {
                Id = request.Id,
                CourseProjectId = request.CourseProjectId,
                Name = request.Name,
                Description = request.Description,
            };

            bool update = await _courseRepository.UpdateCourse(mapDto);

            if (update == false)
            {
                response.Succeeded = false;
                response.Message = "No se pudo actualizar el curso. Contacte con soporte.";

                return response;
            }

            response.Data = update;

            return response;
        }
    }
}

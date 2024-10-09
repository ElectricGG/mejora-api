using MediatR;
using MEJORA.Application.Dtos.Course.Request;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.Course.Commands
{
    public class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand, Response<bool>>
    {
        private readonly ICourseRepository _courseRepository;

        public DeleteCourseHandler(ICourseRepository courseRepository)
            => _courseRepository = courseRepository;

        public async Task<Response<bool>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>(true, "Curso actualizado correctamente.");

            DeleteCrouseRequest mapDto = new()
            {
                Id = request.Id,
                CourseProjectId = request.CourseProjectId,
            };

            bool update = await _courseRepository.DeleteCourse(mapDto);

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

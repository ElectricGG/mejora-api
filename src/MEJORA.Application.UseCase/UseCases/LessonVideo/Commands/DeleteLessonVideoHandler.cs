using MediatR;
using MEJORA.Application.Dtos.LessonVideo.Request;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.LessonVideo.Commands
{
    public class DeleteLessonVideoHandler : IRequestHandler<DeleteLessonVideoCommand, Response<bool>>
    {
        private readonly ILessonVideoRepository _lessonVideoRepository;
        public DeleteLessonVideoHandler(ILessonVideoRepository lessonVideoRepository)
            => _lessonVideoRepository = lessonVideoRepository;

        public async Task<Response<bool>> Handle(DeleteLessonVideoCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>(true, "Video eliminado correctamente.");

            DeleteLessonVideoRequest mapDto = new()
            {
                Id = request.Id
            };

            bool update = await _lessonVideoRepository.DeleteLessonVideo(mapDto);

            if (update == false)
            {
                response.Succeeded = false;
                response.Message = "No se pudo eliminar el video. Contacte con soporte.";

                return response;
            }

            response.Data = update;

            return response;
        }
    }
}

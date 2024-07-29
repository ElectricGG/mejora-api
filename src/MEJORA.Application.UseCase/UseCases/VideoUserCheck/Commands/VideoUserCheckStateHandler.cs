using MediatR;
using MEJORA.Application.Dtos.VideoUserCheck.Request;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.VideoUserCheck.Commands
{
    public class VideoUserCheckStateHandler : IRequestHandler<VideoUserCheckStateCommand, Response<bool>>
    {
        private readonly IVideoUserCheckRepository _videoUserCheckRepository;
        public VideoUserCheckStateHandler(IVideoUserCheckRepository videoUserCheckRepository)
        {
            _videoUserCheckRepository = videoUserCheckRepository;
        }
        public async Task<Response<bool>> Handle(VideoUserCheckStateCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>(true, "Estado actualizado.");

            var mapDto = new VideoUserCheckStateRequest
            {
                CheckedState = request.CheckedState,
                LessonVideoId = request.LessonVideoId,
                UserPersonId = request.UserPersonId,
            };

            var updateEntity = await _videoUserCheckRepository.VideoUserCheckState(mapDto);

            if (!updateEntity)
            {
                response.Succeeded = false;
                response.Message = "No se pudo actualizar. Contacte con soporte.";

                return response;
            }

            return response;
        }
    }
}

using MediatR;
using MEJORA.Application.Dtos.VideoQuestion.Request;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.VideoQuestion.Commands
{
    public class RegisterResponseHandler : IRequestHandler<RegisterResponseCommand, Response<bool>>
    {
        private readonly IVideoQuestionRepository _videoQuestionRepository;
        public RegisterResponseHandler(IVideoQuestionRepository videoQuestionRepository)
        {
            _videoQuestionRepository = videoQuestionRepository;
        }

        public async Task<Response<bool>> Handle(RegisterResponseCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>(true, "Registrado correctamente.");

            var mapDto = new RegisterResponseRequest()
            {
                UserPersonId = request.UserPersonId,
                VideoQuestionId = request.VideoQuestionId,
                Response = request.Response,
            };

            var createEntity = await _videoQuestionRepository.RegisterResponse(mapDto);

            if (!createEntity)
            {
                response.Succeeded = false;
                response.Message = "No se pudo registrar.";

                return response;
            }

            return response;
        }
    }
}

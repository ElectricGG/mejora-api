using MediatR;
using MEJORA.Application.Dtos.VideoQuestion.Request;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Repositories;

namespace MEJORA.Application.UseCase.UseCases.VideoQuestion.Commands
{
    public class RegisterVideoQuestionHandler : IRequestHandler<RegisterVideoQuestionCommand, Response<bool>>
    {
        private readonly IVideoQuestionRepository _videoQuestionRepository;
        public RegisterVideoQuestionHandler(IVideoQuestionRepository videoQuestionRepository)
        {
            _videoQuestionRepository = videoQuestionRepository;
        }
        public async Task<Response<bool>> Handle(RegisterVideoQuestionCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>(true, "Registrado correctamente.");

            var mapDto = new RegisterVideoQuestionRequest()
            {
                UserPersonId = request.UserPersonId,
                LessonVideoId = request.LessonVideoId,
                Comment = request.Comment,
                TimeQuestion = request.TimeQuestion,
            };

            var createEntity = await _videoQuestionRepository.RegisterVideoQuestion(mapDto);

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

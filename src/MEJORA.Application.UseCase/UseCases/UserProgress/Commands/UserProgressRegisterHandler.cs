using MediatR;
using MEJORA.Application.Dtos.UserProgress.Request;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.UserProgress.Commands
{
    public class UserProgressRegisterHandler : IRequestHandler<UserProgressRegisterCommand, Response<bool>>
    {
        private readonly IUserProgressRepository _repository;
        public UserProgressRegisterHandler(IUserProgressRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<bool>> Handle(UserProgressRegisterCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>(true, "Progreso registrado");

            var mapDto = new UserProgressRegisterRequest
            {
                LessonVideoId = request.LessonVideoId,
                UserPersonId = request.UserPersonId,
                SecondsElapsed = request.SecondsElapsed,
            };

            var updateEntity = await _repository.UserProgressRegister(mapDto);

            if (!updateEntity)
            {
                response.Succeeded = false;
                response.Message = "No se pudo registrar el progreso. Contacte con soporte.";

                return response;
            }

            return response;
        }
    }
}

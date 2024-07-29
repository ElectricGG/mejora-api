using MediatR;
using MEJORA.Application.Dtos.LessonVideo.Request;
using MEJORA.Application.Dtos.LessonVideo.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.LessonVideo.Commands
{
    public class CreateLessonVideoHandler : IRequestHandler<CreateLessonVideoCommand, Response<CreateLessonVideoResponse>>
    {
        private readonly ILessonVideoRepository _lessonVideoRepository;
        public CreateLessonVideoHandler(ILessonVideoRepository lessonVideoRepository)
            => _lessonVideoRepository = lessonVideoRepository;

        public async Task<Response<CreateLessonVideoResponse>> Handle(CreateLessonVideoCommand request, CancellationToken cancellationToken)
        {
            var responseDto = new CreateLessonVideoResponse();
            var response = new Response<CreateLessonVideoResponse>(responseDto, "Registrado correctamente.");

            var mapDto = new CreateLessonVideoRequest()
            {
                LessonId = request.LessonId,
                Name = request.Name,
                Description = request.Description,
                UserCreatorId = request.UserCreatorId,
                PlayOrder = request.PlayOrder,
                CourseProjectId = request.CourseProjectId,
                videoFile = request.videoFile,
            };

            var createResponse = await _lessonVideoRepository.CreateLessonVideo(mapDto);
            responseDto.Id = createResponse.Id;
            if (createResponse is null)
            {
                response.Succeeded = false;
                response.Message = "No se pudo registrar.";
                response.Data = createResponse;

                return response;
            }

            return response;
        }
    }
}

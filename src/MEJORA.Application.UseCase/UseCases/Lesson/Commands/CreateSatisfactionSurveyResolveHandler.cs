using MediatR;
using MEJORA.Application.Dtos.Lesson.Request;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;
using MEJORA.Application.UseCase.Services;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Commands
{
    public class CreateSatisfactionSurveyResolveHandler : IRequestHandler<CreateSatisfactionSurveyResolveCommand, Response<CreateSatisfactionSurveyResolveResponse>>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IAzureStorage _azureStorage;

        public CreateSatisfactionSurveyResolveHandler(ILessonRepository lessonRepository, IAzureStorage azureStorage)
        {
            _lessonRepository = lessonRepository;
            _azureStorage = azureStorage;
        }
        public async Task<Response<CreateSatisfactionSurveyResolveResponse>> Handle(CreateSatisfactionSurveyResolveCommand command, CancellationToken cancellationToken)
        {
            var responseDto = new CreateSatisfactionSurveyResolveResponse();

            var codeFile = Guid.NewGuid().ToString();
            var SaveFile = await _azureStorage.SaveFile("surveys", command.file, codeFile);

            var mapDto = new CreateSatisfactionSurveyResolveRequest()
            {
                Code = codeFile,
                Name = command.file.FileName,
                Url = SaveFile.ToString(),
                UserResolveId = command.UserResolveId,
                LessonId = command.LessonId,
                LessonSatisfactionSurveyId = command.LessonSatisfactionSurveyId,
            };

            responseDto = await _lessonRepository.CreateSatisfactionSurveyResolve(mapDto);

            responseDto.Name = mapDto.Name;
            responseDto.Code = mapDto.Code;
            responseDto.Url = mapDto.Url;
            responseDto.UserResolveId = mapDto.UserResolveId;
            responseDto.LessonSatisfactionSurveyId = mapDto.LessonSatisfactionSurveyId;

            var response = new Response<CreateSatisfactionSurveyResolveResponse>(responseDto, "Registrado correctamente.");

            if (responseDto is null)
            {
                response.Succeeded = false;
                response.Message = "No se pudo registrar.";
                response.Data = responseDto;

                return response;
            }



            return response;
        }
    }
}

using MediatR;
using MEJORA.Application.Dtos.Lesson.Request;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;
using MEJORA.Application.UseCase.Services;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Commands
{
    public class CreateSatisfactionSurveyHandler : IRequestHandler<CreateSatisfactionSurveyCommand, Response<CreateSatisfactionSurveyResponse>>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IAzureStorage _azureStorage;

        public CreateSatisfactionSurveyHandler(ILessonRepository lessonRepository, IAzureStorage azureStorage)
        {
            _lessonRepository = lessonRepository;
            _azureStorage = azureStorage;
        }
        public async Task<Response<CreateSatisfactionSurveyResponse>> Handle(CreateSatisfactionSurveyCommand command, CancellationToken cancellationToken)
        {
            var responseDto = new CreateSatisfactionSurveyResponse();


            var codeFile = Guid.NewGuid().ToString();
            var SaveFile = await _azureStorage.SaveFile("surveys", command.file, codeFile);

            var mapDto = new CreateSatisfactionSurveyRequest()
            {
                Code = codeFile,
                Name = command.file.FileName,
                Url = SaveFile.ToString(),
                UserPersonId = command.UserPersonId,
                LessonId = command.LessonId,
            };

            responseDto = await _lessonRepository.CreateSatisfactionSurvey(mapDto);

            responseDto.Name = mapDto.Name;
            responseDto.Code = mapDto.Code;
            responseDto.Url = mapDto.Url;
            responseDto.UserPersonId = mapDto.UserPersonId;

            var response = new Response<CreateSatisfactionSurveyResponse>(responseDto, "Registrado correctamente.");

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

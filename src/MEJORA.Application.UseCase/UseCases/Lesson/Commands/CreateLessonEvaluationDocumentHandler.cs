using MediatR;
using MEJORA.Application.Dtos.Lesson.Request;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;
using MEJORA.Application.UseCase.Services;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Commands
{
    public class CreateLessonEvaluationDocumentHandler : IRequestHandler<CreateLessonEvaluationDocumentCommand, Response<CreateLessonEvaluationDocumentResponse>>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IAzureStorage _azureStorage;

        public CreateLessonEvaluationDocumentHandler(ILessonRepository lessonRepository, IAzureStorage azureStorage)
        {
            _lessonRepository = lessonRepository;
            _azureStorage = azureStorage;
        }
        public async Task<Response<CreateLessonEvaluationDocumentResponse>> Handle(CreateLessonEvaluationDocumentCommand command, CancellationToken cancellationToken)
        {
            var responseDto = new CreateLessonEvaluationDocumentResponse();
            

            var codeFile = Guid.NewGuid().ToString();
            var SaveFile = await _azureStorage.SaveFile("evaluations",command.file, codeFile);

            var mapDto = new CreateLessonEvaluationDocumentRequest()
            {
                Code = codeFile,
                Name = command.file.FileName,
                Url = SaveFile.ToString(),
                UserPersonId = command.UserPersonId,
                LessonId = command.LessonId,
            };

            responseDto = await _lessonRepository.CreateLessonEvaluationDocument(mapDto);

            responseDto.Name = mapDto.Name;
            responseDto.Code = mapDto.Code;
            responseDto.Url = mapDto.Url;
            responseDto.UserPersonId = mapDto.UserPersonId;

            var response = new Response<CreateLessonEvaluationDocumentResponse>(responseDto, "Registrado correctamente.");

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

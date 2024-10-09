using MediatR;
using MEJORA.Application.Dtos.Lesson.Request;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;
using MEJORA.Application.UseCase.Services;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Commands
{
    public class CreateEvaluationResolveHandler : IRequestHandler<CreateEvaluationResolveCommand, Response<CreateEvaluationResolveResponse>>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IAzureStorage _azureStorage;

        public CreateEvaluationResolveHandler(ILessonRepository lessonRepository, IAzureStorage azureStorage)
        {
            _lessonRepository = lessonRepository;
            _azureStorage = azureStorage;
        }
        public async Task<Response<CreateEvaluationResolveResponse>> Handle(CreateEvaluationResolveCommand command, CancellationToken cancellationToken)
        {
            var responseDto = new CreateEvaluationResolveResponse();

            var codeFile = Guid.NewGuid().ToString();
            var SaveFile = await _azureStorage.SaveFile("evaluations", command.file, codeFile);

            var mapDto = new CreateEvaluationResolveRequest()
            {
                Code = codeFile,
                Name = command.file.FileName,
                Url = SaveFile.ToString(),
                UserResolveId = command.UserResolveId,
                LessonId = command.LessonId,
                LessonEvaluationDocumentId = command.LessonEvaluationDocumentId,
            };

            responseDto = await _lessonRepository.CreateEvaluationResolve(mapDto);

            responseDto.Name = mapDto.Name;
            responseDto.Code = mapDto.Code;
            responseDto.Url = mapDto.Url;
            responseDto.UserResolveId = mapDto.UserResolveId;
            responseDto.LessonEvaluationDocumentId = mapDto.LessonEvaluationDocumentId;

            var response = new Response<CreateEvaluationResolveResponse>(responseDto, "Registrado correctamente.");

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

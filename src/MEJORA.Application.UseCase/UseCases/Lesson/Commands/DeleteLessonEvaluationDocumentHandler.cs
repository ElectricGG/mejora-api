using MediatR;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Commands
{
    public class DeleteLessonEvaluationDocumentHandler : IRequestHandler<DeleteLessonEvaluationDocumentCommand, Response<bool>>
    {
        private readonly ILessonRepository _lessonRepository;

        public DeleteLessonEvaluationDocumentHandler(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }
        public async Task<Response<bool>> Handle(DeleteLessonEvaluationDocumentCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>(true, "Eliminado correctamente.");


            var deleteResponse = await _lessonRepository.DeleteLessonEvaluationDocumentn(request.Id);

            if (deleteResponse == false)
            {
                response.Succeeded = false;
                response.Message = "No se pudo deliminar.";
                response.Data = false;

                return response;
            }

            return response;
        }
    }
}

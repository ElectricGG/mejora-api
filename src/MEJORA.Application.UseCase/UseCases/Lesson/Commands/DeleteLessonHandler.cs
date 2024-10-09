using MediatR;
using MEJORA.Application.Dtos.Lesson.Request;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Commands
{
    public class DeleteLessonHandler : IRequestHandler<DeleteLessonCommand, Response<bool>>
    {
        private readonly ILessonRepository _lessonRepository;

        public DeleteLessonHandler(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<Response<bool>> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>(true, "Eliminado correctamente.");

            var mapDto = new DeleteLessonRequest()
            {
                Id = request.Id,
            };

            var deleteResponse = await _lessonRepository.DeleteLesson(mapDto);

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

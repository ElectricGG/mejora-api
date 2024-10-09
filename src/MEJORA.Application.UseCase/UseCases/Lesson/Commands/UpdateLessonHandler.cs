using MediatR;
using MEJORA.Application.Dtos.Lesson.Request;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;
using Org.BouncyCastle.Asn1.Ocsp;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Commands
{
    public class UpdateLessonHandler : IRequestHandler<UpdateLessonCommand, Response<bool>>
    {
        private readonly ILessonRepository _lessonRepository;

        public UpdateLessonHandler(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<Response<bool>> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>(true, "Actualizado correctamente.");

            var mapDto = new UpdateLessonRequest()
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                PreviousImage = request.PreviousImage,
                LessonOrder = request.LessonOrder,
                Objectives = request.Objectives,
                Bibliography = request.Bibliography,
                CvInstructor = request.CvInstructor,
                IndexLesson = request.IndexLesson,
                InstructorName = request.InstructorName,
                InstructorProfession = request.InstructorProfession,
            };

            var updateResponse = await _lessonRepository.UpdateLesson(mapDto);

            if (updateResponse == false)
            {
                response.Succeeded = false;
                response.Message = "No se pudo actualizar.";
                response.Data = false;

                return response;
            }

            return response;
        }
    }
}

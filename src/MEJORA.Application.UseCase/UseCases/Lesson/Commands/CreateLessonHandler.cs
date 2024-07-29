using MediatR;
using MEJORA.Application.Dtos.Lesson.Request;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Commands
{
    public class CreateLessonHandler : IRequestHandler<CreateLessonCommand, Response<CreateLessonResponse>>
    {
        private readonly ILessonRepository _lessonRepository;
        
        public CreateLessonHandler(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<Response<CreateLessonResponse>> Handle(CreateLessonCommand command, CancellationToken cancellationToken)
        {
            var responseDto = new CreateLessonResponse();
            var response = new Response<CreateLessonResponse>(responseDto, "Registrado correctamente.");

            var mapDto =  new CreateLessonRequest()
            {
                CourseProjectId = command.CourseProjectId,
                CourseId = command.CourseId,
                Name = command.Name,
                Description = command.Description,
                UserCreatorId = command.UserCreatorId,
                PreviousImage = command.PreviousImage,
                LessonOrder = command.LessonOrder,
                Objectives = command.Objectives,
                Bibliography = command.Bibliography,
                CvInstructor = command.CvInstructor,
                InstructorName = command.InstructorName,
                InstructorProfession = command.InstructorProfession,
            };

            var createResponse = await _lessonRepository.CreateLesson(mapDto);
            responseDto.Id = createResponse.Id;
            responseDto.Name = createResponse.Name;

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

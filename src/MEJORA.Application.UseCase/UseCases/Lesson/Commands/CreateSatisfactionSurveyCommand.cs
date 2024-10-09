using MediatR;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using Microsoft.AspNetCore.Http;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Commands
{
    public class CreateSatisfactionSurveyCommand : IRequest<Response<CreateSatisfactionSurveyResponse>>
    {
        public int UserPersonId { get; set; }
        public int LessonId { get; set; }
        public IFormFile file { get; set; }
    }
}

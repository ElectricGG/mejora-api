using MediatR;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using Microsoft.AspNetCore.Http;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Commands
{
    public class CreateSatisfactionSurveyResolveCommand : IRequest<Response<CreateSatisfactionSurveyResolveResponse>>
    {
        public int UserResolveId { get; set; }
        public int LessonId { get; set; }
        public int LessonSatisfactionSurveyId { get; set; }
        public IFormFile file { get; set; }
    }
}

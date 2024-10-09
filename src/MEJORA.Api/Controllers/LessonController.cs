using MediatR;
using MEJORA.Application.Dtos.Lesson.Request;
using MEJORA.Application.UseCase.UseCases.Lesson.Commands;
using MEJORA.Application.UseCase.UseCases.Lesson.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MEJORA.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/lesson")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LessonController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("get")]
        public async Task<IActionResult> GetLessonDetail([FromQuery] GetLessonDetailQuery query)
            => Ok(await _mediator.Send(query));

        [HttpGet("list-details")]
        public async Task<IActionResult> ListLessonsDetails([FromQuery] ListLessonDetailsQuery query)
            => Ok(await _mediator.Send(query));

        [HttpPost("upload-evaluation")]
        public async Task<IActionResult> UploadEvaluation([FromForm] CreateLessonEvaluationDocumentCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet("evaluations/get/{lessonId}")]
        public async Task<IActionResult> GetLessonDetail([FromRoute] int lessonId)
            => Ok(await _mediator.Send(new GetLessonEvaluationDocumentByLessonIdQuery { LessonId = lessonId}));

        [HttpDelete("evaluations/{id}")]
        public async Task<IActionResult> DeleteLessonEvaluationDocument([FromRoute] int id)
            => Ok(await _mediator.Send(new DeleteLessonEvaluationDocumentCommand { Id = id }));

        [HttpPost("upload-evaluation-resolve")]
        public async Task<IActionResult> UploadEvaluationResolve([FromForm] CreateEvaluationResolveCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet("evaluations/list/evaluations-resolve")]
        public async Task<IActionResult> ListEvaluationResolves()
            => Ok(await _mediator.Send(new ListEvaluationResolveQuery() { }));

        [HttpPost("upload-satisfaction-survey")]
        public async Task<IActionResult> CreateSatisfactionSurvey([FromForm] CreateSatisfactionSurveyCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet("survey/get/{lessonId}")]
        public async Task<IActionResult> GetSatisfactionSurveyByLessonId([FromRoute] int lessonId)
            => Ok(await _mediator.Send(new GetSatisfactionSurveyByLessonIdQuery { LessonId = lessonId }));

        [HttpDelete("surveys/{id}")]
        public async Task<IActionResult> DeleteSurveys([FromRoute] int id)
            => Ok(await _mediator.Send(new DeleteSatisfactionSurveyCommand { Id = id }));

        [HttpGet("surveys/list/surveys-resolve")]
        public async Task<IActionResult> ListSurveysResolve()
            => Ok(await _mediator.Send(new ListSurveysResolveQuery() { }));

        [HttpPost("upload-satisfaction-survey-resolve")]
        public async Task<IActionResult> CreateSatisfactionSurveyResolve([FromForm] CreateSatisfactionSurveyResolveCommand command)
            => Ok(await _mediator.Send(command));
    }
}

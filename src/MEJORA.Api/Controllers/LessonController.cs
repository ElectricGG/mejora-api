using MediatR;
using MEJORA.Application.UseCase.UseCases.Lesson.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MEJORA.Api.Controllers
{
    [Authorize]
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
    }
}

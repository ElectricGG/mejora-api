using MediatR;
using MEJORA.Application.UseCase.UseCases.LessonVideo.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MEJORA.Api.Controllers
{
    [Authorize]
    [Route("api/lesson/video")]
    [ApiController]
    public class LessonVideoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LessonVideoController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("list")]
        public async Task<IActionResult> ListLessonVideo([FromQuery] ListLessonsVideoQuery query)
            => Ok(await _mediator.Send(query));
    }
}

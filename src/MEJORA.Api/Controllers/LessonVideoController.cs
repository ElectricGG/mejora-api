using MediatR;
using MEJORA.Application.UseCase.UseCases.LessonVideo.Commands;
using MEJORA.Application.UseCase.UseCases.LessonVideo.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MEJORA.Api.Controllers
{
    [AllowAnonymous]
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

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateLessonVideoCommand command)
            => Ok(await _mediator.Send(command));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
            => Ok(await _mediator.Send(new DeleteLessonVideoCommand { Id = id }));

        [HttpGet("listByLessonId")]
        public async Task<IActionResult> ListLessonVideiByLessonId([FromQuery] ListLessonVideoByLessonIdQuery query)
            => Ok(await _mediator.Send(query));
    }
}

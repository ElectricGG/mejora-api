using MediatR;
using MEJORA.Application.UseCase.UseCases.Course.Commands;
using MEJORA.Application.UseCase.UseCases.CourseLesson.Queries;
using MEJORA.Application.UseCase.UseCases.Lesson.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MEJORA.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/course/lesson")]
    [ApiController]
    public class CourseLessonController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CourseLessonController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("most-viewed")]
        public async Task<IActionResult> ListTheMostView([FromQuery] TheMostViewedLessonsQuery query)
            => Ok(await _mediator.Send(query));

        [HttpGet("newly-uploaded")]
        public async Task<IActionResult> ListNewlyUploaded([FromQuery] NewlyUploadedQuery query)
            => Ok(await _mediator.Send(query));

        [HttpGet("user-watching")]
        public async Task<IActionResult> ListUserWatching([FromQuery] UserWatchingQuery query)
            => Ok(await _mediator.Send(query));

        [HttpGet("list")]
        public async Task<IActionResult> CourseLessonListByName([FromQuery] ListByNameQuery query)
            => Ok(await _mediator.Send(query));

        [HttpGet]
        public async Task<IActionResult> ListLessonByCourseId([FromQuery] ListLessonByCourseIdQuery query)
            => Ok(await _mediator.Send(query));

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateLessonCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateLessonCommand command)
            => Ok(await _mediator.Send(command));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
            => Ok(await _mediator.Send(new DeleteLessonCommand { Id = id }));
    }
}

using MediatR;
using MEJORA.Application.UseCase.UseCases.Course.Commands;
using MEJORA.Application.UseCase.UseCases.Course.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MEJORA.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CourseController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery]ListCoursesQuery query)
            => Ok(await _mediator.Send(query));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet("list/admin")]
        public async Task<IActionResult> ListCoursesAdmin([FromQuery] ListCoursesAdminQuery query)
            => Ok(await _mediator.Send(query));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCourseCommand command)
            => Ok(await _mediator.Send(command));

        [HttpDelete("{id}/{courseProjectId}")]
        public async Task<IActionResult> Delete([FromRoute] int id, [FromRoute] int courseProjectId)
            => Ok(await _mediator.Send(new DeleteCourseCommand { Id = id, CourseProjectId = courseProjectId }));
    }
}

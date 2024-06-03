using MediatR;
using MEJORA.Application.UseCase.UseCases.Course.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MEJORA.Api.Controllers
{
    [Authorize]
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
    }
}

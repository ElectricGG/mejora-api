using MediatR;
using MEJORA.Application.UseCase.UseCases.VideoUserCheck.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MEJORA.Api.Controllers
{
    [Authorize]
    [Route("api/video/user/check")]
    [ApiController]
    public class VideoUserCheckController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VideoUserCheckController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> VideoUserCheckState([FromBody]VideoUserCheckStateCommand stateCommand)
            => Ok(await _mediator.Send(stateCommand));
    }
}

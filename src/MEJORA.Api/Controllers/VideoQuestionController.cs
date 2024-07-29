using MediatR;
using MEJORA.Application.UseCase.UseCases.VideoQuestion.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MEJORA.Api.Controllers
{
    [Route("api/video/question")]
    [ApiController]
    public class VideoQuestionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VideoQuestionController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> RegisterVideoQuestion([FromBody] RegisterVideoQuestionCommand command)
            => Ok(await _mediator.Send(command));
    }
}

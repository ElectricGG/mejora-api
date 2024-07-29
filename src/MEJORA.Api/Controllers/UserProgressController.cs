using MediatR;
using MEJORA.Application.UseCase.UseCases.UserProgress.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MEJORA.Api.Controllers
{
    [Route("api/user/progress")]
    [ApiController]
    public class UserProgressController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserProgressController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> UserProgressRegister([FromBody]UserProgressRegisterCommand stateCommand)
            => Ok(await _mediator.Send(stateCommand));
    }
}

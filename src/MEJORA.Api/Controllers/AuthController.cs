using MediatR;
using MEJORA.Application.UseCase.UseCases.Auth.Commands.RecoveryPwdCommand;
using MEJORA.Application.UseCase.UseCases.Auth.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MEJORA.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
            => _mediator = mediator;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
            => Ok(await _mediator.Send(query));

        [AllowAnonymous]
        [HttpPost("recoverypwd")]
        public async Task<IActionResult> Recovery([FromQuery] RecoveryPwdCommand command)
            => Ok(await _mediator.Send(command));
    }
}

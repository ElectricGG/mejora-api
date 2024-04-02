using MediatR;
using MEJORA.Application.UseCase.UseCases.Auth.Commands.RecoveryPwdCommand;
using MEJORA.Application.UseCase.UseCases.UserPerson.CreateCommand;
using MEJORA.Application.UseCase.UseCases.UserPerson.ValidateEmailCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MEJORA.Api.Controllers
{
    
    [Route("api/user/person")]
    [ApiController]
    public class UserPersonController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserPersonController(IMediator mediator)
            => _mediator = mediator;

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateUserPersonCommand command)
            => Ok(await _mediator.Send(command));

        [AllowAnonymous]
        [HttpPost("validate")]
        public async Task<IActionResult> ValidationEmail([FromQuery] ValidateEmailCommand command)
            => Ok(await _mediator.Send(command));

        
    }
}

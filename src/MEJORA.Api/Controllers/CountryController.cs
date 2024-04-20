using MediatR;
using MEJORA.Application.UseCase.UseCases.Country.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MEJORA.Api.Controllers
{
    [Route("api/country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CountryController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("select")]
        public async Task<IActionResult> Select([FromQuery] SelectCountryQuery query)
            => Ok(await _mediator.Send(query));
    }
}

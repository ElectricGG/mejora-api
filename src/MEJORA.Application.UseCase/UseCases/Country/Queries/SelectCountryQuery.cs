using MediatR;
using MEJORA.Application.Dtos.Country.Response;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.Country.Queries
{
    public class SelectCountryQuery : IRequest<Response<SelectCountryResponse>>
    {
    }
}

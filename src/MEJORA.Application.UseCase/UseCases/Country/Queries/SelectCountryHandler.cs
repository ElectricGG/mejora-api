using MediatR;
using MEJORA.Application.Dtos.Country.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.Country.Queries
{
    public class SelectCountryHandler : IRequestHandler<SelectCountryQuery, Response<SelectCountryResponse>>
    {
        private readonly ICountryRepository _repository;
        public SelectCountryHandler(ICountryRepository repository)
            => _repository = repository;

        public async Task<Response<SelectCountryResponse>> Handle(SelectCountryQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.SelectCountryResponse();
            return new Response<SelectCountryResponse>(response);
        }
    }
}

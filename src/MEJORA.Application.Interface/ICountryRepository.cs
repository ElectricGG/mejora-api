using MEJORA.Application.Dtos.Country.Response;

namespace MEJORA.Application.Interface
{
    public interface ICountryRepository
    {
        Task<List<SelectCountryResponse>> SelectCountryResponse();
    }
}

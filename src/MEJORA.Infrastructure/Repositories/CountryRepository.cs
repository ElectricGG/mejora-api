using Dapper;
using MEJORA.Application.Dtos.Country.Response;
using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Context;
using System.Data;

namespace MEJORA.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDdContext _context;
        public CountryRepository(ApplicationDdContext context)
            => _context = context;

        public async Task<List<SelectCountryResponse>> SelectCountryResponse()
        {
            using var connection = _context.CreateConnection;
            string procedure = "spSelectCountry";

            var response = await connection.QueryAsync<SelectCountryResponse>(
                procedure,
                commandType: CommandType.StoredProcedure
            );

            return response.ToList();
        }
    }
}

using Dapper;
using MEJORA.Application.Dtos.Country.Response;
using MEJORA.Application.Dtos.UserPerson.Response;
using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Context;
using MEJORA.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using Dapper;
using MEJORA.Application.Dtos.UserPerson.Request;
using MEJORA.Application.Dtos.UserPerson.Response;
using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Context;
using MEJORA.Utilities.Constants;
using System.Data;

namespace MEJORA.Infrastructure.Repositories
{
    public class UserPersonRespository : IUserPersonRespository
    {
        private readonly ApplicationDdContext _context;
        public UserPersonRespository(ApplicationDdContext context)
        {
            _context = context;
        }

        public async Task<GetUserPersonByEmailResponse> GetUserPersonByEmail(GetUserPersonByEmailRequest request)
        {
            using var connection = _context.CreateConnection;
            string procedure = StoredProcedure.spGetUserPersonByEmail;

            var parametros = new DynamicParameters();
            parametros.Add("email", request.Email);

            var response = await connection.QueryFirstOrDefaultAsync<GetUserPersonByEmailResponse>(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            return response;
        }

        public async Task<GetUserPersonByUsernameResponse> GetUserPersonByUsername(GetUserPersonByUsernameRequest request)
        {
            using var connection = _context.CreateConnection;
            string procedure = StoredProcedure.spGetUserPersonByUsername;

            var parametros = new DynamicParameters();
            parametros.Add("username", request.Username);

            var response = await connection.QueryFirstOrDefaultAsync<GetUserPersonByUsernameResponse>(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            return response;
        }

        public async Task<CreateUserPersonResponse> CreateUserPerson(CreateUserPersonRequest request)
        {
            using var connection = _context.CreateConnection;
            string procedure = StoredProcedure.spCreateUserPerson;

            var parametros = new DynamicParameters();
            parametros.Add("FirstName", request.FirstName);
            parametros.Add("LastName", request.LastName);
            parametros.Add("UserName", request.UserName);
            parametros.Add("Email", request.Email);
            parametros.Add("Password", request.Password);
            parametros.Add("CountryId", request.CountryId);
            parametros.Add("NewUserId", DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("GuidId", Guid.NewGuid());

            var result = await connection.ExecuteAsync(
                            procedure,
                            param: parametros,
                            commandType: CommandType.StoredProcedure
                        );

            int newUserId = parametros.Get<int>("@NewUserId");

            string sql = @"
                                SELECT id, firstname, lastname, username, email
                                FROM user_person
                                WHERE id = @NewUserId
                            ";

            
            var userPerson = await connection.QuerySingleOrDefaultAsync<CreateUserPersonResponse>(
                sql,
                new { NewUserId = newUserId }
            );

            return userPerson;
        }

        public async Task<bool> ValidateEmail(string identifier)
        {
            using var connection = _context.CreateConnection;
            string procedure = "spValidateEmail";

            var parametros = new DynamicParameters();
            parametros.Add("@identifier", identifier);
            parametros.Add("@updated", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            var response = parametros.Get<bool>("@updated");

            return response;
        }

        public async Task<GetUserPersonByEmailResponse> RecoveryPassword(string email)
        {
            using var connection = _context.CreateConnection;
            string procedure = StoredProcedure.spGetUserPersonByEmail;

            var parametros = new DynamicParameters();
            parametros.Add("email", email);

            var response = await connection.QueryFirstOrDefaultAsync<GetUserPersonByEmailResponse>(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            return response;
        }

        public async Task<bool> UpdateRecoveryPwd(UpdateRecoveryPasswordRequest request)
        {
            using var connection = _context.CreateConnection;
            string procedure = "spUpdateRecoveryPwd";

            var parametros = new DynamicParameters();
            parametros.Add("@userPersonId", request.UserPersonId);
            parametros.Add("@password", request.Password);
            parametros.Add("@updated", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            var response = parametros.Get<bool>("@updated");

            return response;
        }
    }
}

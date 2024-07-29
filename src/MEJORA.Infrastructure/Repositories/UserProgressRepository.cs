using Dapper;
using MEJORA.Application.Dtos.UserProgress.Request;
using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Context;
using System.Data;

namespace MEJORA.Infrastructure.Repositories
{
    public class UserProgressRepository : IUserProgressRepository
    {
        private readonly ApplicationDdContext _context;
        public UserProgressRepository(ApplicationDdContext context)
            => _context = context;
        public async Task<bool> UserProgressRegister(UserProgressRegisterRequest request)
        {
            using var connection = _context.CreateConnection;
            string procedure = "spUserProgressRegister";

            var parametros = new DynamicParameters();
            parametros.Add("@LessonVideoId", request.LessonVideoId);
            parametros.Add("@UserPersonId", request.UserPersonId);
            parametros.Add("@SecondsElapsed", request.SecondsElapsed);

            var affectedRows = await connection.ExecuteAsync(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            return affectedRows > 0;
        }
    }
}

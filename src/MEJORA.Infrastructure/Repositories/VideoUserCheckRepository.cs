using Dapper;
using MEJORA.Application.Dtos.VideoUserCheck.Request;
using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Context;
using System.Data;

namespace MEJORA.Infrastructure.Repositories
{
    public class VideoUserCheckRepository : IVideoUserCheckRepository
    {
        private readonly ApplicationDdContext _context;
        public VideoUserCheckRepository(ApplicationDdContext context)
            => _context = context;

        public async Task<bool> VideoUserCheckState(VideoUserCheckStateRequest request)
        {
            using var connection = _context.CreateConnection;
            string procedure = "spVideoUserCheckState";

            var parametros = new DynamicParameters();
            parametros.Add("@LessonVideoId", request.LessonVideoId);
            parametros.Add("@UserPersonId", request.UserPersonId);
            parametros.Add("@CheckedState", request.CheckedState);

            var affectedRows = await connection.ExecuteAsync(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            return affectedRows > 0;
        }
    }
}

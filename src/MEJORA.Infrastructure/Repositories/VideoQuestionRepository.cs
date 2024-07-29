using Dapper;
using MEJORA.Application.Dtos.VideoQuestion.Request;
using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Context;
using System.Data;

namespace MEJORA.Infrastructure.Repositories
{
    public class VideoQuestionRepository : IVideoQuestionRepository
    {
        private readonly ApplicationDdContext _context;
        public VideoQuestionRepository(ApplicationDdContext context)
            => _context = context;

        public async Task<bool> RegisterVideoQuestion(RegisterVideoQuestionRequest request)
        {
            using var connection = _context.CreateConnection;
            string procedure = "spRegisterVideoQuestion";

            var parametros = new DynamicParameters();
            parametros.Add("@UserPersonId", request.UserPersonId);
            parametros.Add("@LessonVideoId", request.LessonVideoId);
            parametros.Add("@Comment", request.Comment);
            parametros.Add("@TimeQuestion", request.TimeQuestion);

            var affectedRows = await connection.ExecuteAsync(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            return affectedRows > 0;
        }
    }
}

using Dapper;
using MEJORA.Application.Dtos.Lesson.Request;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Context;
using System.Data;

namespace MEJORA.Infrastructure.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly ApplicationDdContext _context;
        public LessonRepository(ApplicationDdContext context)
            => _context = context;
        public async Task<GetLessonDetailResponse> GetLessonDetail(GetLessonDetailRequest request)
        {
            using var connection = _context.CreateConnection;
            string procedure = "spGetLessonDetail";

            var parametros = new DynamicParameters();
            parametros.Add("@LessonId", request.LessonId);

            var response = await connection.QueryFirstOrDefaultAsync<GetLessonDetailResponse>(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            return response;
        }
    }
}

using Dapper;
using MEJORA.Application.Dtos.LessonVideo.Request;
using MEJORA.Application.Dtos.LessonVideo.Response;
using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Context;
using System.Data;

namespace MEJORA.Infrastructure.Repositories
{
    public class LessonVideoRepository : ILessonVideoRepository
    {
        private readonly ApplicationDdContext _context;
        public LessonVideoRepository(ApplicationDdContext context)
            => _context = context;
        public async Task<List<ListLessonsVideoResponse>> ListLessonByCourseId(ListLessonsVideoRequest request)
        {
            using var connection = _context.CreateConnection;
            string procedure = "spListLessonsVideo";

            var parametros = new DynamicParameters();
            parametros.Add("@LessonId", request.LessonId);

            var response = await connection.QueryAsync<ListLessonsVideoResponse>(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            return response.ToList();
        }
    }
}

using Dapper;
using MEJORA.Application.Dtos.Course.Response;
using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Context;
using System.Data;

namespace MEJORA.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDdContext _context;
        public CourseRepository(ApplicationDdContext context)
            => _context = context;

        public async Task<List<ListCoursesResponse>> ListCourses()
        {
            using var connection = _context.CreateConnection;
            string procedure = "spListCoursesMenu";

            var response = await connection.QueryAsync<ListCoursesResponse>(
                procedure,
                commandType: CommandType.StoredProcedure
            );

            return response.ToList();
        }
    }
}

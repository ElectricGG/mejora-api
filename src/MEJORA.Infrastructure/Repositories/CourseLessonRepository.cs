using Dapper;
using MEJORA.Application.Dtos.CourseLesson.Request;
using MEJORA.Application.Dtos.CourseLesson.Response;
using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Context;
using System.Data;

namespace MEJORA.Infrastructure.Repositories
{
    public class CourseLessonRepository : ICourseLessonRepository
    {
        private readonly ApplicationDdContext _context;
        public CourseLessonRepository(ApplicationDdContext context)
            => _context = context;

        public async Task<List<CourseLessonNewlyUploadedResponse>> CourseLessonNewlyUploaded()
        {
            using var connection = _context.CreateConnection;
            string procedure = "spLessonNewlyUploaded";

            var response = await connection.QueryAsync<CourseLessonNewlyUploadedResponse>(
                procedure,
                commandType: CommandType.StoredProcedure
            );

            return response.ToList();
        }

        public async Task<List<CourseLessonMostViewResponse>> ListCourseLessonMostView()
        {
            using var connection = _context.CreateConnection;
            string procedure = "spTheMostViewedLessons";

            var response = await connection.QueryAsync<CourseLessonMostViewResponse>(
                procedure,
                commandType: CommandType.StoredProcedure
            );

            return response.ToList();
        }

        public async Task<List<CourseLessonUserWatchingResponse>> CourseLessonUserWatching(CourseLessonUserWatchingRequest request)
        {
            using var connection = _context.CreateConnection;
            string procedure = "spUserWatching";

            var parametros = new DynamicParameters();
            parametros.Add("@user_person_id", request.UserPersonId);

            var response = await connection.QueryAsync<CourseLessonUserWatchingResponse>(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            return response.ToList();
        }

        public async Task<List<CourseLessonListResponse>> CourseLessonListByName(CourseLessonListRequest request)
        {
            using var connection = _context.CreateConnection;
            string procedure = "spListLessonsByName";

            var parametros = new DynamicParameters();
            if (string.IsNullOrEmpty(request.Name))
            {
                parametros.Add("@Name", "");
            }
            else
            {
                parametros.Add("@Name", request.Name);
            }
            

            var response = await connection.QueryAsync<CourseLessonListResponse>(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            return response.ToList();
        }

        public async Task<List<ListLessonByCourseResponse>> ListLessonByCourseId(ListLessonByCourseRequest request)
        {
            using var connection = _context.CreateConnection;
            string procedure = "spListLessonByCourseId";

            var parametros = new DynamicParameters();
            parametros.Add("@CourseId", request.CourseId);

            var response = await connection.QueryAsync<ListLessonByCourseResponse>(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            return response.ToList();
        }
    }
}

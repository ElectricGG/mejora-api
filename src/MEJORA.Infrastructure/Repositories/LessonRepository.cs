using Dapper;
using MEJORA.Application.Dtos.Course.Response;
using MEJORA.Application.Dtos.Lesson.Request;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wistia.Request;
using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Server;
using System.Data;

namespace MEJORA.Infrastructure.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly ApplicationDdContext _context;
        private readonly IWistiaRepository _wistiaRepository;
        private readonly IConfiguration _configuration;
        public LessonRepository(ApplicationDdContext context, IWistiaRepository wistiaRepository, IConfiguration configuration)
            => (_context, _wistiaRepository, _configuration) = (context, wistiaRepository,configuration);

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

        public async Task<CreateLessonResponse> CreateLesson(CreateLessonRequest request)
        {
            var responseDto = new CreateLessonResponse();
            var baseUrl = _configuration["UrlBase:urlMejora"]!;
            var urlLessonsImg = _configuration["UrlBase:urlLessonsImg"]!;
            
            using (var connection = _context.CreateConnection)
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    string spCreateLesson = "spCreateLesson";
                    string spCreateCourseLesson = "spCreateCourseLesson";
                    string spSetImageLesson = "spSetImageLesson";
                    
                    var paramCreateLesson = new DynamicParameters();
                    paramCreateLesson.Add("@Name", request.Name);
                    paramCreateLesson.Add("@Description", request.Description);
                    paramCreateLesson.Add("@DurationMinutes", 0);
                    paramCreateLesson.Add("@Url", "");
                    paramCreateLesson.Add("@UserCreatorId", request.UserCreatorId);
                    paramCreateLesson.Add("@PreviousImage", "");
                    paramCreateLesson.Add("@LessonOrder", request.LessonOrder);
                    paramCreateLesson.Add("@Objectives", request.Objectives);
                    paramCreateLesson.Add("@Bibliography", request.Bibliography);
                    paramCreateLesson.Add("@CvInstructor", request.CvInstructor);
                    paramCreateLesson.Add("@IndexLesson", request.IndexLesson);
                    paramCreateLesson.Add("@InstructorName", request.InstructorName);
                    paramCreateLesson.Add("@InstructorProfession", request.InstructorProfession);
                    paramCreateLesson.Add("@DurationSeconds", 0);

                    var lessonIdResult = await connection.QuerySingleAsync<int>(
                        spCreateLesson,
                        paramCreateLesson,
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure
                    );

                    if(lessonIdResult <= 0)
                    {
                        transaction.Rollback();
                        return responseDto;
                    }

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "Images\\Lessons");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // Genera un nombre único para la imagen
                    var imageName = $"{lessonIdResult}{Path.GetExtension(request.PreviousImage.FileName)}";
                    var imagePath = Path.Combine(path, imageName);

                    // Guarda la imagen en el directorio
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await request.PreviousImage.CopyToAsync(stream);
                    }

                    var paramCreateLessonIMG = new DynamicParameters();
                    paramCreateLessonIMG.Add("@PreviousImage", baseUrl+ urlLessonsImg+ imageName);
                    paramCreateLessonIMG.Add("@Id", lessonIdResult);

                    var lessonIdResultImg = await connection.QuerySingleAsync<int>(
                        spSetImageLesson,
                        paramCreateLessonIMG,
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure
                    );

                    if (lessonIdResultImg <= 0)
                    {
                        transaction.Rollback();
                        return responseDto;
                    }

                    #region RELACION COURSE LESSON
                    var paramCreateCourseLesson = new DynamicParameters();
                    paramCreateCourseLesson.Add("@CourseId", request.CourseId);
                    paramCreateCourseLesson.Add("@LessonId", lessonIdResult);
                    paramCreateCourseLesson.Add("@UserCreatorId", request.UserCreatorId);

                    var courseLessonIdResult = await connection.QuerySingleAsync<int>(
                        spCreateCourseLesson,
                        paramCreateCourseLesson,
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure
                    );

                    if(courseLessonIdResult <= 0)
                    {
                        transaction.Rollback();
                        return responseDto;
                    }
                    #endregion


                    responseDto.Id = lessonIdResult;

                    transaction.Commit();

                    return responseDto;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error creating lessons", ex);
                }
            }
        }

        public async Task<List<ListLessonsDetailsResponse>> ListLessonsDetails(ListLessonsDetailsRequest request)
        {
            using var connection = _context.CreateConnection;

            string procedure = "spListLessonByCourseId";
            var parametros = new DynamicParameters();
            parametros.Add("@CourseId", request.CourseId);

            var response = (await connection.QueryAsync<ListLessonsDetailsResponse>(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            )).ToList();

            foreach (var lesson in response)
            {
                var parametrosvideo = new DynamicParameters();
                parametrosvideo.Add("@LessonId", lesson.Id);

                var responseVideos = (await connection.QueryAsync<ListLessonVideoDetailsResponse>(
                    "spListLessonsVideoByLesson",
                    param: parametrosvideo,
                    commandType: CommandType.StoredProcedure
                )).ToList();

                lesson.listLessonVideoDetailsResponses = responseVideos;
            }

            return response;
        }
    }
}

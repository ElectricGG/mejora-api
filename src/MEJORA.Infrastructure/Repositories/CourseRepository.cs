using Dapper;
using MEJORA.Application.Dtos.Course.Request;
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

        public async Task<CreateCourseResponse> CreateCourses(CreateCourseRequest request)
        {
            var response = new CreateCourseResponse();

            using (var connection = _context.CreateConnection)
            {
                // Iniciar transacción
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    string spCreateCourse = "spCreateCourse";
                    string spCreateLesson = "spCreateLesson";
                    string spCreateCourseLesson = "spCreateCourseLesson";
                    string spCreateLessonVideo = "spCreateLessonVideo";
                    string spCreateVideoUserCheck = "spCreateVideoUserCheck";

                    var paramCreateCourse = new DynamicParameters();
                    paramCreateCourse.Add("@Name", request.Name);
                    paramCreateCourse.Add("@Description", request.Description);
                    paramCreateCourse.Add("@CourseLevelId", request.CourseLevelId);
                    paramCreateCourse.Add("@UserCreatorId", request.UserCreatorId);
                    paramCreateCourse.Add("@PreviousImage", request.PreviousImage);

                    var courseIdResult = await connection.QuerySingleAsync<int>(
                            spCreateCourse,
                            paramCreateCourse,
                            transaction: transaction,
                            commandType: CommandType.StoredProcedure
                        );

                    foreach (var courseLessonDto in request.createLessonRequest)
                    {
                        var paramCreateLesson = new DynamicParameters();
                        paramCreateLesson.Add("@Name", courseLessonDto.Name);
                        paramCreateLesson.Add("@Description", courseLessonDto.Description);
                        paramCreateLesson.Add("@DurationMinutes", 0);
                        paramCreateLesson.Add("@Url", "");
                        paramCreateLesson.Add("@UserCreatorId", courseLessonDto.UserCreatorId);
                        paramCreateLesson.Add("@PreviousImage", courseLessonDto.PreviousImage);
                        paramCreateLesson.Add("@LessonOrder", courseLessonDto.LessonOrder);
                        paramCreateLesson.Add("@Objectives", courseLessonDto.Objectives);
                        paramCreateLesson.Add("@Bibliography", courseLessonDto.Bibliography);
                        paramCreateLesson.Add("@CvInstructor", courseLessonDto.CvInstructor);
                        paramCreateLesson.Add("@IndexLesson", courseLessonDto.IndexLesson);
                        paramCreateLesson.Add("@InstructorName", courseLessonDto.InstructorName);
                        paramCreateLesson.Add("@InstructorProfession", courseLessonDto.InstructorProfession);
                        paramCreateLesson.Add("@DurationSeconds", "");

                        var lessonIdResult = await connection.QuerySingleAsync<int>(
                            spCreateLesson,
                            paramCreateLesson,
                            transaction: transaction,
                            commandType: CommandType.StoredProcedure
                        );

                        #region RELACION COURSE LESSON
                        var paramCreateCourseLesson = new DynamicParameters();
                        paramCreateCourseLesson.Add("@CourseId", courseIdResult);
                        paramCreateCourseLesson.Add("@LessonId", lessonIdResult);
                        paramCreateCourseLesson.Add("@UserCreatorId", courseLessonDto.UserCreatorId);

                        var courseLessonIdResult = await connection.QuerySingleAsync<int>(
                            spCreateCourseLesson,
                            paramCreateCourseLesson,
                            transaction: transaction,
                            commandType: CommandType.StoredProcedure
                        );
                        #endregion

                        //foreach (var lessonVideoDto in courseLessonDto.createLessonVideoRequests)
                        //{
                        //    var paramLessonVideo = new DynamicParameters();
                        //    paramLessonVideo.Add("@LessonId", lessonIdResult);
                        //    paramLessonVideo.Add("@Name", lessonVideoDto.Name);
                        //    paramLessonVideo.Add("@Description", lessonVideoDto.Description);
                        //    paramLessonVideo.Add("@HtmlContent", "");
                        //    paramLessonVideo.Add("@UserCreatorId", lessonVideoDto.UserCreatorId);
                        //    paramLessonVideo.Add("@Hours", lessonVideoDto.Hours);
                        //    paramLessonVideo.Add("@Mins", lessonVideoDto.Mins);
                        //    paramLessonVideo.Add("@Sec", lessonVideoDto.Sec);
                        //    paramLessonVideo.Add("@PlayOrder", lessonVideoDto.PlayOrder);

                        //    var lessonVideoIdResult = await connection.QuerySingleAsync<int>(
                        //        spCreateLessonVideo,
                        //        paramLessonVideo,
                        //        transaction: transaction,
                        //        commandType: CommandType.StoredProcedure
                        //    );

                        //    #region INSERCION PARA PODER GUARDAR EL CHECK POR CADA VIDEO
                            
                        //    var paramVideoUserCheck = new DynamicParameters();
                        //    paramLessonVideo.Add("@UserPersonId", lessonVideoDto.UserCreatorId);
                        //    paramLessonVideo.Add("@LessonVideoId", lessonVideoIdResult);
                        //    paramLessonVideo.Add("@CheckState", 0);

                        //    var videoUserCheckIdResult = await connection.QuerySingleAsync<int>(
                        //        spCreateVideoUserCheck,
                        //        paramVideoUserCheck,
                        //        transaction: transaction,
                        //        commandType: CommandType.StoredProcedure
                        //    );

                        //    #endregion
                        //}
                    }

                    // Crear la respuesta con el ID devuelto
                    response = new CreateCourseResponse
                    {
                        Id = courseIdResult,
                        Name = request.Name,
                    };

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error creating courses", ex);
                }
            }

            return response;
        }

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

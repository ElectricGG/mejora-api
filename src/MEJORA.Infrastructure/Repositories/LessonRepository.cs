using Dapper;
using MEJORA.Application.Dtos.Course.Request;
using MEJORA.Application.Dtos.Lesson.Request;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.Transactions;

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

        public async Task<bool> UpdateLesson(UpdateLessonRequest request)
        {
            using (var connection = _context.CreateConnection)
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    var baseUrl = _configuration["UrlBase:urlMejora"]!;
                    var urlLessonsImg = _configuration["UrlBase:urlLessonsImg"]!;

                    string spUpdateLesson = "spUpdateLesson";

                    var param = new DynamicParameters();
                    param.Add("@LessonId", request.Id);
                    param.Add("@Name", request.Name);
                    param.Add("@@Description", request.Description);
                    param.Add("@PreviousImage", "");
                    param.Add("@LessonOrder", request.LessonOrder);
                    param.Add("@Objetives", request.Objectives);
                    param.Add("@Bibliography", request.Bibliography);
                    param.Add("@CvInstructor", request.CvInstructor);
                    param.Add("@IndexLesson", request.IndexLesson);
                    param.Add("@InstructorName", request.InstructorName);
                    param.Add("@InstructorProfession", request.InstructorProfession);

                    // Ejecutar el procedimiento y capturar el número de filas afectadas
                    var affectedRows = await connection.ExecuteScalarAsync<int>(
                        spUpdateLesson,
                        param,
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure
                    );


                    #region imagen
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "Images\\Lessons");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // Genera un nombre único para la imagen
                    var imageName = $"{request.Id}{Path.GetExtension(request.PreviousImage.FileName)}";
                    var imagePath = Path.Combine(path, imageName);

                    // Guarda la imagen en el directorio
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await request.PreviousImage.CopyToAsync(stream);
                    }

                    string spSetImageLesson = "spSetImageLesson";

                    var paramCreateLessonIMG = new DynamicParameters();
                    paramCreateLessonIMG.Add("@PreviousImage", baseUrl + urlLessonsImg + imageName);
                    paramCreateLessonIMG.Add("@Id", request.Id);

                    var lessonIdResultImg = await connection.QuerySingleAsync<int>(
                        spSetImageLesson,
                        paramCreateLessonIMG,
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure
                    );

                    if (lessonIdResultImg <= 0)
                    {
                        transaction.Rollback();
                        return false;
                    }
                    #endregion

                    string spListHashedVideoByLessonId = "spListHashedVideoByLessonId";

                    // Obtener la lista de lecciones que se deben eliminar
                    var lessonsToDelete = (await connection.QueryAsync<ListHashedLessonVideoByIdResponse>(
                        spListHashedVideoByLessonId,
                        new { LessonId = request.Id },
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure
                    )).ToList();

                    // Verificar si la lista está vacía
                    if (lessonsToDelete == null || !lessonsToDelete.Any())
                    {
                        // Manejar el caso donde no hay lecciones que eliminar
                        // Puedes retornar un valor, lanzar una excepción personalizada, o simplemente no hacer nada.
                    }
                    else
                    {
                        // Recorre el listado de lecciones y eliminar cada una
                        foreach (var hasheds in lessonsToDelete)
                        {
                            await _wistiaRepository.UpdateMedia(hasheds.Hashed_Id, request.Name + " - " + hasheds.Name);
                        }
                    }

                    transaction.Commit();
                    // Retornar true si se afectó al menos una fila, false en caso contrario
                    return affectedRows > 0;


                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<bool> DeleteLesson(DeleteLessonRequest request)
        {
            using (var connection = _context.CreateConnection)
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    string spDeleteLesson = "spDeleteLesson";

                    var param = new DynamicParameters();
                    param.Add("@LessonId", request.Id);

                    // Ejecutar el procedimiento y capturar el número de filas afectadas
                    var affectedRows = await connection.ExecuteScalarAsync<int>(
                        spDeleteLesson,
                        param,
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure
                    );

                    if (affectedRows > 0)
                    {
                        string spListHashedVideoByLessonId = "spListHashedVideoByLessonId";

                        // Obtener la lista de lecciones que se deben eliminar
                        var lessonsToDelete = (await connection.QueryAsync<ListHashedLessonVideoByIdResponse>(
                            spListHashedVideoByLessonId,
                            new { LessonId = request.Id },
                            transaction: transaction,
                            commandType: CommandType.StoredProcedure
                        )).ToList();

                        // Verificar si la lista está vacía
                        if (lessonsToDelete == null || !lessonsToDelete.Any())
                        {
                            // Manejar el caso donde no hay lecciones que eliminar
                            // Puedes retornar un valor, lanzar una excepción personalizada, o simplemente no hacer nada.
                        }
                        else
                        {
                            // Recorre el listado de lecciones y eliminar cada una
                            foreach (var hasheds in lessonsToDelete)
                            {
                                await _wistiaRepository.DeleteMedia(hasheds.Hashed_Id);
                            }
                        }
                    }

                    transaction.Commit();
                    // Retornar true si se afectó al menos una fila, false en caso contrario
                    return affectedRows > 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            
        }

        public async Task<CreateLessonEvaluationDocumentResponse> CreateLessonEvaluationDocument(CreateLessonEvaluationDocumentRequest request)
        {
            string spCreateLessonEvaluationDocument = "spCreateLessonEvaluationDocument";

            using var connection = _context.CreateConnection;

            var param = new DynamicParameters();
            param.Add("@Name", request.Name);
            param.Add("@Code", request.Code);
            param.Add("@Url", request.Url);
            param.Add("@UserPersonId", request.UserPersonId);
            param.Add("@LessonId", request.LessonId);

            var generatedId = await connection.QuerySingleAsync<int>(
                spCreateLessonEvaluationDocument,
                param,
                commandType: CommandType.StoredProcedure
            );

            return new CreateLessonEvaluationDocumentResponse
            {
                Id = generatedId
            };
        }

        public async Task<GetLessonEvaluationDocumentByLessonIdResponse> getLessonEvaluationDocumentByLessonId(int LessonId)
        {
            using var connection = _context.CreateConnection;
            string procedure = "spGetLessonEvaluationDocumentByLessonId";

            var parametros = new DynamicParameters();
            parametros.Add("@LessonId", LessonId);

            var response = await connection.QueryFirstOrDefaultAsync<GetLessonEvaluationDocumentByLessonIdResponse>(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            return response;
        }

        public async Task<bool> DeleteLessonEvaluationDocumentn(int id)
        {
            using var connection = _context.CreateConnection;
            string procedure = "spDeleteLessonEvaluationDocument";

            var parametros = new DynamicParameters();
            parametros.Add("@Id", id);

            var result = await connection.ExecuteScalarAsync<int>(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            // Devuelve true si se actualizó al menos una fila, de lo contrario false
            return result == 1;
        }

        public async Task<CreateEvaluationResolveResponse> CreateEvaluationResolve(CreateEvaluationResolveRequest request)
        {
            string spCreateLessonEvaluationDocumentResolve = "spCreateLessonEvaluationDocumentResolve";

            using var connection = _context.CreateConnection;

            var param = new DynamicParameters();
            param.Add("@Name", request.Name);
            param.Add("@Code", request.Code);
            param.Add("@Url", request.Url);
            param.Add("@UserResolverId", request.UserResolveId);
            param.Add("@LessonId", request.LessonId);
            param.Add("@LessonEvaluationDocumentId", request.LessonEvaluationDocumentId);

            var generatedId = await connection.QuerySingleAsync<int>(
                spCreateLessonEvaluationDocumentResolve,
                param,
                commandType: CommandType.StoredProcedure
            );

            return new CreateEvaluationResolveResponse
            {
                Id = generatedId
            };
        }

        public async Task<List<ListEvaluationResolveResponse>> ListEvaluationResolves()
        {
            using var connection = _context.CreateConnection;

            string procedure = "spListEvaluationsResolve";

            var response = (await connection.QueryAsync<ListEvaluationResolveResponse>(
                procedure,
                commandType: CommandType.StoredProcedure
            )).ToList();

            return response;
        }

        public async Task<CreateSatisfactionSurveyResponse> CreateSatisfactionSurvey(CreateSatisfactionSurveyRequest request)
        {
            try
            {
                string spCreateSatisfactionSurvey = "spCreateSatisfactionSurvey";

                using var connection = _context.CreateConnection;

                var param = new DynamicParameters();
                param.Add("@Name", request.Name);
                param.Add("@Code", request.Code);
                param.Add("@Url", request.Url);
                param.Add("@UserCreatorId", request.UserPersonId);
                param.Add("@LessonId", request.LessonId);

                var generatedId = await connection.QuerySingleAsync<int>(
                    spCreateSatisfactionSurvey,
                    param,
                    commandType: CommandType.StoredProcedure
                );

                return new CreateSatisfactionSurveyResponse
                {
                    Id = generatedId
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GetSatisfactionSurveyByLessonIdResponse> GetSatisfactionSurveyByLessonId(int LessonId)
        {
            using var connection = _context.CreateConnection;
            string procedure = "spGetSatisfactionSurveyByLessonId";

            var parametros = new DynamicParameters();
            parametros.Add("@LessonId", LessonId);

            var response = await connection.QueryFirstOrDefaultAsync<GetSatisfactionSurveyByLessonIdResponse>(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            return response;
        }

        public async Task<bool> DeleteSatisfactionSurvey(int id)
        {
            using var connection = _context.CreateConnection;
            string procedure = "spDeleteSatisfactionSurvey";

            var parametros = new DynamicParameters();
            parametros.Add("@Id", id);

            var result = await connection.ExecuteScalarAsync<int>(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            // Devuelve true si se actualizó al menos una fila, de lo contrario false
            return result == 1;
        }

        public async Task<List<ListSurveysResolveResponse>> ListSurveysResolves()
        {
            using var connection = _context.CreateConnection;

            string procedure = "spListSurveysResolve";

            var response = (await connection.QueryAsync<ListSurveysResolveResponse>(
                procedure,
                commandType: CommandType.StoredProcedure
            )).ToList();

            return response;
        }

        public async Task<CreateSatisfactionSurveyResolveResponse> CreateSatisfactionSurveyResolve(CreateSatisfactionSurveyResolveRequest request)
        {
            string spCreateSatisfactionSurveyResolve = "spCreateSatisfactionSurveyResolve";

            using var connection = _context.CreateConnection;

            var param = new DynamicParameters();
            param.Add("@Name", request.Name);
            param.Add("@Code", request.Code);
            param.Add("@Url", request.Url);
            param.Add("@UserResolverId", request.UserResolveId);
            param.Add("@LessonId", request.LessonId);
            param.Add("@LessonSatisfactionSurveyId", request.LessonSatisfactionSurveyId);

            var generatedId = await connection.QuerySingleAsync<int>(
                spCreateSatisfactionSurveyResolve,
                param,
                commandType: CommandType.StoredProcedure
            );

            return new CreateSatisfactionSurveyResolveResponse
            {
                Id = generatedId
            };
        }
    }
}

using Dapper;
using FFMpegCore;
using MEJORA.Application.Dtos.LessonVideo.Request;
using MEJORA.Application.Dtos.LessonVideo.Response;
using MEJORA.Application.Dtos.Wistia.Request;
using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Context;
using System.Data;

namespace MEJORA.Infrastructure.Repositories
{
    public class LessonVideoRepository : ILessonVideoRepository
    {
        private readonly ApplicationDdContext _context;
        private readonly IWistiaRepository _wistiaRepository;
        public LessonVideoRepository(ApplicationDdContext context, IWistiaRepository wistiaRepository)
            => (_context, _wistiaRepository) = (context, wistiaRepository);

        public async Task<CreateLessonVideoResponse> CreateLessonVideo(CreateLessonVideoRequest request)
        {
            var responseDto = new CreateLessonVideoResponse();
            using (var connection = _context.CreateConnection)
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Guarda el archivo temporalmente
                    var filePath = Path.GetTempFileName();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.videoFile.CopyToAsync(stream);
                    }

                    // Carga el archivo de video y obtiene los metadatos
                    var mediaInfo = await FFProbe.AnalyseAsync(filePath);
                    var duration = mediaInfo.Duration;

                    // Elimina el archivo temporal
                    File.Delete(filePath);

                    string spCreateLessonVideo = "spCreateLessonVideo";
                    string spCreateVideoUserCheck = "spCreateVideoUserCheck";
                    string spSetHtmlContentIntoLessonVideo = "spSetHtmlContentIntoLessonVideo";

                    var paramLessonVideo = new DynamicParameters();
                    paramLessonVideo.Add("@LessonId", request.LessonId);
                    paramLessonVideo.Add("@Name", request.Name);
                    paramLessonVideo.Add("@Description", request.Description);
                    paramLessonVideo.Add("@UserCreatorId", request.UserCreatorId);
                    paramLessonVideo.Add("@Hours", duration.Hours);
                    paramLessonVideo.Add("@Mins", duration.Minutes);
                    paramLessonVideo.Add("@Sec", duration.Seconds);
                    paramLessonVideo.Add("@PlayOrder", request.PlayOrder);

                    var lessonVideoResult = await connection.QuerySingleAsync<LessonVideoResultResponse>(
                        spCreateLessonVideo,
                        paramLessonVideo,
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure
                    );

                    int lessonVideoIdResult = lessonVideoResult.NewLessonVideoId;
                    string NameLesson = lessonVideoResult.NameLesson;

                    if (lessonVideoIdResult <= 0)
                    {
                        transaction.Rollback();
                        return responseDto;
                    }

                    var requestMedia = new UploadMediaRequest()
                    {
                        FilePath = request.videoFile,
                        ProjectId = request.CourseProjectId?.ToString() ?? 0.ToString(),
                        Name = NameLesson + " - " +request.Name,
                        Description = request.Description,
                    };

                    var uploadMediaResponse = await _wistiaRepository.UploadMedia(requestMedia);

                    var paramUpdateLv = new DynamicParameters();
                    paramUpdateLv.Add("@HashedId", uploadMediaResponse.Hashed_Id);
                    paramUpdateLv.Add("@LessonVideoId", lessonVideoIdResult);
                    paramUpdateLv.Add("@WistiaId", uploadMediaResponse.Id);

                    await connection.ExecuteAsync(
                                                spSetHtmlContentIntoLessonVideo,
                                                paramUpdateLv,
                                                transaction: transaction,
                                                commandType: CommandType.StoredProcedure
                                            );

                    #region INSERCION PARA PODER GUARDAR EL CHECK POR CADA VIDEO

                    var paramVideoUserCheck = new DynamicParameters();
                    paramVideoUserCheck.Add("@UserPersonId", request.UserCreatorId);
                    paramVideoUserCheck.Add("@LessonVideoId", lessonVideoIdResult);
                    paramVideoUserCheck.Add("@CheckState", 0);

                    var videoUserCheckIdResult = await connection.QuerySingleAsync<int>(
                        spCreateVideoUserCheck,
                        paramVideoUserCheck,
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure
                    );

                    if (videoUserCheckIdResult <= 0)
                    {
                        transaction.Rollback();
                        return responseDto;
                    }

                    #endregion

                    responseDto.Id = lessonVideoIdResult;

                    transaction.Commit();

                    return responseDto;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    using var connection2 = _context.CreateConnection;
                    string procedure = "spCreateLogExceptions";

                    var parametros = new DynamicParameters();
                    parametros.Add("@result", ex.Message + " ___ " + ex.InnerException + " ___ " + ex.StackTrace);

                    var affectedRows = await connection.ExecuteAsync(
                        procedure,
                        param: parametros,
                        commandType: CommandType.StoredProcedure
                    );
                    throw new Exception("Error creating medias : " + ex);
                }
            }
        }

        public async Task<List<ListLessonsVideoResponse>> ListLessonByCourseId(ListLessonsVideoRequest request)
        {
            using var connection = _context.CreateConnection;
            string procedure = "spListLessonsVideo";

            var parametros = new DynamicParameters();
            parametros.Add("@LessonId", request.LessonId);
            parametros.Add("@UserPersonId", request.UserPersonId);

            var response = await connection.QueryAsync<ListLessonsVideoResponse>(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            return response.ToList();
        }


        public async Task<bool> DeleteLessonVideo(DeleteLessonVideoRequest request)
        {
            try
            {
                using var connection = _context.CreateConnection;

                string spDeleteLessonVideo = "spDeleteLessonVideo";

                var param = new DynamicParameters();
                param.Add("@LessonVideoId", request.Id);

                // Ejecutar el procedimiento y capturar el número de filas afectadas
                var affectedRows = await connection.ExecuteScalarAsync<int>(
                    spDeleteLessonVideo,
                    param,
                    commandType: CommandType.StoredProcedure
                );

                string spGetLessonVideoHashedById = "spGetLessonVideoHashedById";

                var paramHashed = new DynamicParameters();
                paramHashed.Add("@LessonVideoId", request.Id);

                // Ejecutar el procedimiento y capturar el valor devuelto como string
                string hashedId = await connection.QuerySingleAsync<string>(
                    spGetLessonVideoHashedById,
                    paramHashed,
                    commandType: CommandType.StoredProcedure
                );

                await _wistiaRepository.DeleteMedia(hashedId);

                return affectedRows > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ListLessonVideoByLessonIdResponse>> ListLessonVideoByLessonId(ListLessonVideoByLessonIdRequest request)
        {
            using var connection = _context.CreateConnection;
            string procedure = "spListLessonVideoByLessonId";

            var parametros = new DynamicParameters();
            parametros.Add("@LessonId", request.LessonId);

            var response = await connection.QueryAsync<ListLessonVideoByLessonIdResponse>(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            return response.ToList();
        }
    }
}

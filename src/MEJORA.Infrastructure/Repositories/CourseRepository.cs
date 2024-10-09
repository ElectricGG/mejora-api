using Dapper;
using MEJORA.Application.Dtos.Course.Request;
using MEJORA.Application.Dtos.Course.Response;
using MEJORA.Application.Dtos.LessonVideo.Request;
using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Context;
using System.Data;
using System.Transactions;

namespace MEJORA.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDdContext _context;
        private readonly IWistiaRepository _wistiaRepository;
        public CourseRepository(ApplicationDdContext context, IWistiaRepository wistiaRepository)
            => (_context, _wistiaRepository) = (context, wistiaRepository);

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
                    string spUpdateCourseProjectId = "spUpdateCourseProjectId";

                    var paramCreateCourse = new DynamicParameters();
                    paramCreateCourse.Add("@Name", request.Name);
                    paramCreateCourse.Add("@Description", request.Description);
                    paramCreateCourse.Add("@UserCreatorId", request.UserCreatorId);

                    var courseIdResult = await connection.QuerySingleAsync<int>(
                            spCreateCourse,
                            paramCreateCourse,
                            transaction: transaction,
                            commandType: CommandType.StoredProcedure
                        );

                    var createProject = await _wistiaRepository.CreateProject(new CreateProjectRequest { Name = request.Name });

                    if(createProject == null || createProject.Id == 0)
                    {
                        transaction.Rollback();
                        throw new Exception("Error creating courses");
                    }

                    

                    var paramUpdateCourseProjectId = new DynamicParameters();
                    paramUpdateCourseProjectId.Add("@CourseId", courseIdResult);
                    paramUpdateCourseProjectId.Add("@CourseProjectId", createProject.Id);

                    var updateCourseProjectIdResult = await connection.QuerySingleAsync<int>(
                            spUpdateCourseProjectId,
                            paramUpdateCourseProjectId,
                            transaction: transaction,
                            commandType: CommandType.StoredProcedure
                        );

                    // Crear la respuesta con el ID devuelto
                    response = new CreateCourseResponse
                    {
                        Id = courseIdResult,
                        Name = request.Name,
                        CourseProjectId = createProject.Id
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

        public async Task<List<ListCoursesAdminResponse>> ListCoursesAdmin()
        {
            using var connection = _context.CreateConnection;
            string procedure = "spListCoursesAdmin";

            var response = await connection.QueryAsync<ListCoursesAdminResponse>(
                procedure,
                commandType: CommandType.StoredProcedure
            );

            return response.ToList();
        }

        public async Task<bool> UpdateCourse(UpdateCourseRequest request)
        {
            try
            {
                using var connection = _context.CreateConnection;

                string spUpdateCourse = "spUpdateCourse";

                var param = new DynamicParameters();
                param.Add("@CourseId", request.Id);
                param.Add("@Name", request.Name);
                param.Add("@Descripcion", request.Description);

                // Ejecutar el procedimiento y capturar el número de filas afectadas
                var affectedRows = await connection.ExecuteScalarAsync<int>(
                    spUpdateCourse,
                    param,
                    commandType: CommandType.StoredProcedure
                );

                var requestUpdate = new UpdateProjectRequest
                {
                    Id = request.CourseProjectId.ToString(),
                    Name = request.Name,
                    Description = request.Description
                };

                if (affectedRows > 0)
                {
                    var createProject = await _wistiaRepository.UpdateProject(requestUpdate);
                }

                // Retornar true si se afectó al menos una fila, false en caso contrario
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteCourse(DeleteCrouseRequest request)
        {
            try
            {
                using var connection = _context.CreateConnection;

                string spDeleteCourse = "spDeleteCourse";

                var param = new DynamicParameters();
                param.Add("@CourseId", request.Id);

                // Ejecutar el procedimiento y capturar el número de filas afectadas
                var affectedRows = await connection.ExecuteScalarAsync<int>(
                    spDeleteCourse,
                    param,
                    commandType: CommandType.StoredProcedure
                );

                var requestDelete = new DeleteProjectRequest
                {
                    Id = request.CourseProjectId.ToString()
                };

                if (affectedRows > 0)
                {
                    var deleteProject = await _wistiaRepository.DeleteProject(requestDelete);

                    if(deleteProject is not null)
                    {
                        string spListLessonsToDelete = "spListLessonsToDelete";
                        // Obtener la lista de lecciones que se deben eliminar
                        var lessonsToDelete = await connection.QueryAsync<string>(
                            spListLessonsToDelete,
                            new { CourseId = request.Id },
                            commandType: CommandType.StoredProcedure
                        );

                        // Recorre el listado de lecciones y eliminar cada una
                        foreach (var hashed_id in lessonsToDelete)
                        {
                            await _wistiaRepository.DeleteMedia(hashed_id);
                        }
                    }
                }

                // Retornar true si se afectó al menos una fila, false en caso contrario
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

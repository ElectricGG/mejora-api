﻿using Dapper;
using MEJORA.Application.Dtos.VideoQuestion.Request;
using MEJORA.Application.Dtos.VideoQuestion.Response;
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

        public async Task<List<ListVideoQuestionResponse>> ListQuestionVideo()
        {
            using var connection = _context.CreateConnection;

            string procedure = "spListQuestionVideo";

            var response = (await connection.QueryAsync<ListVideoQuestionResponse>(
                procedure,
                commandType: CommandType.StoredProcedure
            )).ToList();

            return response;
        }

        public async Task<bool> RegisterResponse(RegisterResponseRequest request)
        {
            using var connection = _context.CreateConnection;
            string procedure = "spRegisterResponse";

            var parametros = new DynamicParameters();
            parametros.Add("@UserPersonId", request.UserPersonId);
            parametros.Add("@Response", request.Response);
            parametros.Add("@VideoQuestionId", request.VideoQuestionId);

            var affectedRows = await connection.ExecuteAsync(
                procedure,
                param: parametros,
                commandType: CommandType.StoredProcedure
            );

            return affectedRows > 0;
        }

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

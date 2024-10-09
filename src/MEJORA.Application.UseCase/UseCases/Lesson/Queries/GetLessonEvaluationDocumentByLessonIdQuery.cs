﻿using MediatR;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Queries
{
    public class GetLessonEvaluationDocumentByLessonIdQuery : IRequest<Response<GetLessonEvaluationDocumentByLessonIdResponse>>
    {
        public int LessonId { get; set; }
    }
}
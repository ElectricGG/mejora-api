﻿namespace MEJORA.Application.Dtos.Lesson.Request
{
    public class CreateSatisfactionSurveyResolveRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int UserResolveId { get; set; }
        public int LessonId { get; set; }
        public int LessonSatisfactionSurveyId { get; set; }
    }
}
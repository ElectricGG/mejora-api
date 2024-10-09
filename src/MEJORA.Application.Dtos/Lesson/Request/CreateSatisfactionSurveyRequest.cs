﻿namespace MEJORA.Application.Dtos.Lesson.Request
{
    public class CreateSatisfactionSurveyRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int UserPersonId { get; set; }
        public int LessonId { get; set; }
    }
}
﻿namespace MEJORA.Application.Dtos.Course.Request
{
    public class UpdateCourseRequest
    {
        public int Id { get; set; }
        public int CourseProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

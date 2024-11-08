﻿namespace MEJORA.Application.Dtos.Lesson.Response
{
    public class GetLessonByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PreviousImage { get; set; }
        public int LessonOrder { get; set; }
        public string Objectives { get; set; }
        public string Bibliography { get; set; }
        public string InstructorName { get; set; }
        public string InstructorProfession { get; set; }
        public string CvInstructor { get; set; }
    }
}

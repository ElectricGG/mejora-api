﻿namespace MEJORA.Application.Dtos.Auth.Response
{
    public class LoginResponse
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}
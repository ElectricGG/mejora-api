﻿namespace MEJORA.Application.Dtos.UserPerson.Response
{
    public class CreateUserPersonResponse
    {
        public int Id {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Guid { get; set; }
    }
}

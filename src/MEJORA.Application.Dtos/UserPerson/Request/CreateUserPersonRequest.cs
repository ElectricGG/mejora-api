namespace MEJORA.Application.Dtos.UserPerson.Request
{
    public class CreateUserPersonRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CountryId { get; set; }
    }
}

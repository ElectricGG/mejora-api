using MediatR;
using MEJORA.Application.Dtos.UserPerson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.UserPerson.CreateCommand
{
    public class CreateUserPersonCommand : IRequest<Response<CreateUserPersonResponse>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CountryId { get; set; }
        public string Phone_number { get; set; }
    }
}

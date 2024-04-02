using MEJORA.Application.Dtos.UserPerson.Request;
using MEJORA.Application.Dtos.UserPerson.Response;

namespace MEJORA.Application.Interface
{
    public interface IUserPersonRespository
    {
        Task<GetUserPersonByEmailResponse> GetUserPersonByEmail(GetUserPersonByEmailRequest request);
        Task<GetUserPersonByUsernameResponse> GetUserPersonByUsername(GetUserPersonByUsernameRequest request);
        Task<CreateUserPersonResponse> CreateUserPerson(CreateUserPersonRequest request);
        Task<bool> ValidateEmail(string identifier);
        Task<GetUserPersonByEmailResponse> RecoveryPassword(string email);
        Task<bool> UpdateRecoveryPwd(UpdateRecoveryPasswordRequest request);
    }
}

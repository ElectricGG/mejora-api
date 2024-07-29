using MEJORA.Application.Dtos.UserProgress.Request;

namespace MEJORA.Application.Interface
{
    public interface IUserProgressRepository
    {
        Task<bool> UserProgressRegister(UserProgressRegisterRequest request);

    }
}

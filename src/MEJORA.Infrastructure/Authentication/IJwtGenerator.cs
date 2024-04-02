using MEJORA.Application.Dtos.Auth.Request;

namespace MEJORA.Infrastructure.Authentication
{
    public interface IJwtGenerator
    {
        string GenerateToken(ClaimnsRequest request);
    }
}

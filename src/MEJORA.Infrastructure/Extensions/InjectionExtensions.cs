using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Authentication;
using MEJORA.Infrastructure.Context;
using MEJORA.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MEJORA.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionPersistence(this IServiceCollection services)
        {
            services.AddSingleton<ApplicationDdContext>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddSingleton<IUserPersonRespository, UserPersonRespository>();

            return services;
        }
    }
}

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
            services.AddSingleton<ICountryRepository, CountryRepository>();
            services.AddSingleton<ICourseRepository, CourseRepository>();
            services.AddSingleton<ICourseLessonRepository, CourseLessonRepository>();
            services.AddSingleton<ILessonRepository, LessonRepository>();
            services.AddSingleton<ILessonVideoRepository, LessonVideoRepository>();

            return services;
        }
    }
}

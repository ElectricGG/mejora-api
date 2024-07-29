using MEJORA.Application.Interface;
using MEJORA.Infrastructure.Authentication;
using MEJORA.Infrastructure.Context;
using MEJORA.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http; // Asegúrate de tener esto

namespace MEJORA.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ApplicationDdContext>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddSingleton<IConfiguration>(configuration);

            services.AddSingleton<IUserPersonRespository, UserPersonRespository>();
            services.AddSingleton<ICountryRepository, CountryRepository>();
            services.AddSingleton<ICourseRepository, CourseRepository>();
            services.AddSingleton<ICourseLessonRepository, CourseLessonRepository>();
            services.AddSingleton<ILessonRepository, LessonRepository>();
            services.AddSingleton<ILessonVideoRepository, LessonVideoRepository>();
            services.AddSingleton<IVideoUserCheckRepository, VideoUserCheckRepository>();
            services.AddSingleton<IUserProgressRepository, UserProgressRepository>();
            services.AddSingleton<IVideoQuestionRepository, VideoQuestionRepository>();
            services.AddSingleton<IWistiaRepository, WistiaRepository>();

            // Configurar HttpClient para WistiaService
            services.AddHttpClient<IWistiaRepository, WistiaRepository>();

            return services;
        }
    }
}

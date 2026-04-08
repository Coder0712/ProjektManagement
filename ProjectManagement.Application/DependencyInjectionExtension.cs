using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Application.Services;

namespace ProjectManagement.Application
{
    /// <summary>
    /// Represents an extension class for the DI configuration.
    /// </summary>
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// Registers the application services.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns></returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<ICardsService, CardService>();
            return services;
        }
    }
}

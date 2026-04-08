using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Domain.Boards;
using ProjectManagement.Domain.Cards;
using ProjectManagement.Domain.Common;
using ProjectManagement.Domain.Projects;
using ProjectManagement.Domain.Services;
using ProjectManagement.Infrastructure.Options;
using ProjectManagement.Infrastructure.Repositories;
using ProjectManagement.Infrastructure.Services;

namespace ProjectManagement.Infrastructure
{
    /// <summary>
    /// Represents an extension class for the DI configuration.
    /// </summary>
    public static class DependencyExtensions
    {
        /// <summary>
        /// Registers all components of the persistence project.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions<DatabaseOptionsSetup>();
            services.AddDbContext<IDbContext, ManagementDbContext>(options =>
            {
                options.UseNpgsql(GetConnectionString(configuration));
                options.AddInterceptors(new AuditableInterceptor());
            });

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IBoardRepository, BoardRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IBoardProjectUniquessChecker, BoardProjectUniquessChecker>();
            services.AddScoped<IProjectTitleUniquenessChecker, ProjectTitleUniquenessChecker>();

            return services;
        }

        /// <summary>
        /// Migrate the database to the latest version. This method should be called at the application startup.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/>.</param>
        public static void MigrateDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ManagementDbContext>();
            dbContext.Database.Migrate();
        }

        /// <summary>
        ///  Gets the connection string of the database.
        /// </summary>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>The connection string.</returns>
        public static string GetConnectionString(IConfiguration configuration)
        {
            var option = new DatabaseOptions();

            var optionsSetup = new DatabaseOptionsSetup(configuration);

            optionsSetup.Configure(option);

            return option.ConnectionString;
        }
    }
}

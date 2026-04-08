using Microsoft.Extensions.Hosting;
using Serilog;

namespace ProjectManagement.Infrastructure.Logging
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
        public static IHostBuilder AddSerilog(this IHostBuilder hostBuilder)
            => hostBuilder.UseSerilog((hostingContext, configuration) =>
            {
                configuration.ReadFrom
                .Configuration(hostingContext.Configuration)
                .WriteTo.Console();
            });
    }
}

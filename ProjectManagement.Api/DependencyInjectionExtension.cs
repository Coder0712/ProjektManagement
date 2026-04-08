using FluentValidation;
using Microsoft.AspNetCore.HttpLogging;
using ProjectManagement.Api.Validators;
using System.Text.Json.Serialization;

namespace ProjectManagement.Api
{
    /// <summary>
    /// Represents an extension class for the DI configuration.
    /// </summary>
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// Registers the API services.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns></returns>
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    });
            services.AddOpenApi();
            services.AddValidatorsFromAssemblyContaining<CreateProjectRequestValidator>();
            services.AddProblemDetails(options =>
            {
                options.CustomizeProblemDetails = pdc =>
                {
                    pdc.ProblemDetails.Instance = null;
                    pdc.ProblemDetails.Type = null;
                    pdc.ProblemDetails.Extensions.Remove("traceId");
                };
            });
            services.AddHttpLogging(options =>
            {
                options.LoggingFields = HttpLoggingFields.RequestMethod
                    | HttpLoggingFields.RequestPath
                    | HttpLoggingFields.RequestBody
                    | HttpLoggingFields.ResponseStatusCode
                    | HttpLoggingFields.Duration;
            });

            return services;
        }
    }
}

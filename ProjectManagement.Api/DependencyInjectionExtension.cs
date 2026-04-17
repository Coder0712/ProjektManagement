using FluentValidation;
using Microsoft.AspNetCore.HttpLogging;
using ProjectManagement.Api.Validators;
using ProjectManagement.Infrastructure.Options;
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
            services.ConfigureOptions<AuthenticationOptionsSetup>();
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    });
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    document.Components ??= new Microsoft.OpenApi.Models.OpenApiComponents();

                    document.Components.SecuritySchemes["bearerAuth"] =
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                            Scheme = "bearer",
                            BearerFormat = "JWT",
                            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                            Description = "JWT Authorization header using the Bearer scheme."
                        };

                    document.SecurityRequirements.Add(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                    {
                        {
                            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                            {
                                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                                {
                                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                    Id = "bearerAuth"
                                }
                            }, Array.Empty<string>()
                        }
                    });

                    return Task.CompletedTask;

                });
            });

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

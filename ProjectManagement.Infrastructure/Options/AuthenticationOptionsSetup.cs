using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ProjectManagement.Infrastructure.Options
{
    /// <summary>
    /// Reads the data in the appsettings.
    /// </summary>
    public sealed class AuthenticationOptionsSetup
        : IConfigureOptions<AuthenticationOptions>
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initialize a new object of type <see cref="AuthenticationOptionsSetup"/>.>
        /// </summary>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        public AuthenticationOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public void Configure(AuthenticationOptions options)
        {
            _configuration.GetSection(nameof(AuthenticationOptions)).Bind(options);
        }
    }
}
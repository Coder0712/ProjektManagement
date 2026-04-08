using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ProjectManagement.Infrastructure.Options
{
    /// <summary>
    /// Reads the data in the appsettings.
    /// </summary>
    public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initialize a new object of type <see cref="KanbanBoardService"/>.>
        /// </summary>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        public DatabaseOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public void Configure(DatabaseOptions options)
        {
            _configuration.GetSection(nameof(DatabaseOptions)).Bind(options);
        }
    }
}

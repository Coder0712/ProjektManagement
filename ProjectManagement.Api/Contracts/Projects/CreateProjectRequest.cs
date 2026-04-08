namespace ProjectManagement.Api.Contracts.Projects
{
    /// <summary>
    /// Represents a request to create a project.
    /// </summary>
    public sealed record CreateProjectRequest
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public required string Description { get; init; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public required string Status { get; init; }
    }
}

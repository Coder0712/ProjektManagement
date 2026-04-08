namespace ProjectManagement.Api.Contracts.Projects
{
    /// <summary>
    /// Represents a request to update an existing project.
    /// </summary>
    public sealed record UpdateProjectRequest
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string? Name { get; init; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        public string? Status { get; init; }
    }
}

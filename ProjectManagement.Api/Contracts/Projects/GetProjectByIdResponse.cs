using ProjectManagement.Domain.Projects;

namespace ProjectManagement.Api.Contracts.Projects
{
    /// <summary>
    /// Represents the response for getting a project by its id.
    /// </summary>
    public sealed record GetProjectByIdResponse
    {
        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        public required Project Project { get; set; }
    }
}

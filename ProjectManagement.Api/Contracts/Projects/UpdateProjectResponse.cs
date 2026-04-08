using ProjectManagement.Application.Responses.Project;

namespace ProjectManagement.Api.Contracts.Projects
{
    /// <summary>
    /// Represents the response for updating a project.
    /// </summary>
    public sealed record UpdateProjectResponse
    {
        /// <summary>
        /// Gets or sets the updated project.
        /// </summary>
        public required ProjectResponse Project { get; set; }
    }
}

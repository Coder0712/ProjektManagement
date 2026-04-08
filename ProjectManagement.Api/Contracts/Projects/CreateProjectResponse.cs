using ProjectManagement.Application.Responses.Project;

namespace ProjectManagement.Contracts.Projects
{
    public sealed record CreateProjectResponse
    {
        /// <summary>
        /// Gets or sets the project
        /// </summary>
        public required ProjectResponse Project { get; set; }
    }
}

using ProjectManagement.Application.Responses.Project;

namespace ProjectManagement.Api.Contracts.Projects
{
    /// <summary>
    /// Represents a response containing a list of projects with pagination information.
    /// </summary>
    public sealed record GetAllProjectsResponse
    {
        /// <summary>
        /// Gets the page.
        /// </summary>
        public required int Page { get; init; }

        /// <summary>
        /// Gets the items.
        /// </summary>
        public required int Items { get; init; }

        /// <summary>
        /// Gets the number of all items.
        /// </summary>
        public required int TotalCount { get; init; }

        /// <summary>
        /// Gets or sets projects.
        /// </summary>
        public required List<ProjectResponse> Projects { get; set; }
    }
}

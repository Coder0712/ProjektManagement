namespace ProjectManagement.Application.Responses.Project
{
    /// <summary>
    /// Represents the project paging response.
    /// </summary>
    public sealed record ProjectPagingResponse
    {
        /// <summary>
        /// Gets the page.
        /// </summary>
        public int Page { get; init; }

        /// <summary>
        /// Gets the items per page.
        /// </summary>
        public int Items { get; init; }

        /// <summary>
        /// Gets the total number of items.
        /// </summary>
        public int TotalCount { get; init; }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        public List<ProjectResponse> Projects { get; init; }
    }
}

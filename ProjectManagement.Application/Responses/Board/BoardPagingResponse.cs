namespace ProjectManagement.Application.Responses.Board
{
    /// <summary>
    /// Represents a paginated response containing a collection of boards and pagination metadata.
    /// </summary>
    public sealed record BoardPagingResponse
    {
        /// <summary>
        /// Gets the page.
        /// </summary>
        public int Page { get; init; }

        /// <summary>
        /// Gets the number of items per page.
        /// </summary>
        public int Items { get; init; }

        /// <summary>
        /// Gets the total count.
        /// </summary>
        public int TotalCount { get; init; }

        /// <summary>
        /// Gets the collection of boards returned in the response.
        /// </summary>
        public List<BoardResponse> Boards { get; init; }
    }
}

using ProjectManagement.Application.Responses.Board;

namespace ProjectManagement.Api.Contracts.Boards
{
    /// <summary>
    /// Represents a response containing a list of all boards with pagination information.
    /// </summary>
    public sealed record GetAllBoardsResponse
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
        /// Gets or sets boards.
        /// </summary>
        public required List<BoardResponse> Boards { get; set; }
    }
}

using ProjectManagement.Application.Responses.Board;

namespace ProjectManagement.Api.Contracts.Boards
{
    /// <summary>
    /// Represents the response to update a board.
    /// </summary>
    public sealed record UpdateBoardResponse
    {
        /// <summary>
        /// Gets or sets the board.
        /// </summary>
        public required BoardResponse Board { get; set; }
    }
}

using ProjectManagement.Domain.Boards;

namespace ProjectManagement.Api.Contracts.Boards
{
    /// <summary>
    /// Represents the response for creating a board.
    /// </summary>
    public sealed record CreateBoardResponse
    {
        /// <summary>
        /// Gets or sets the board.
        /// </summary>
        public required Board Board { get; set; }
    }
}

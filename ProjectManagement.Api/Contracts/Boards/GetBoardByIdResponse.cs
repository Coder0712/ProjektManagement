using ProjectManagement.Domain.Boards;

namespace ProjectManagement.Api.Contracts.Boards
{
    /// <summary>
    /// Represents the response for getting a board by its id.
    /// </summary>
    public sealed record GetBoardByIdResponse
    {
        /// <summary>
        /// Gets or sets the board.
        /// </summary>
        public required Board Board { get; set; }
    }
}

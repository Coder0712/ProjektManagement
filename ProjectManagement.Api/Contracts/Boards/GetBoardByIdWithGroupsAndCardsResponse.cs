using ProjectManagement.Application.Responses.Board;

namespace ProjectManagement.Api.Contracts.Boards
{
    /// <summary>
    /// Represents the response for getting a board by its id with groups and cards.
    /// </summary>
    public sealed record GetBoardByIdWithGroupsAndCardsResponse
    {
        /// <summary>
        /// Gets or sets the board with groups and cards.
        /// </summary>
        public required BoardWithGroupsAndCardsResponse Board { get; set; }
    }
}

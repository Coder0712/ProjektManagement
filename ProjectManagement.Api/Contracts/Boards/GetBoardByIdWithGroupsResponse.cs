using ProjectManagement.Application.Responses.Board;

namespace ProjectManagement.Api.Contracts.Boards
{
    /// <summary>
    /// Represents the response for getting a board by its id with groups.
    /// </summary>
    public sealed record GetBoardByIdWithGroupsResponse
    {
        /// <summary>
        /// Gets or sets the board with groups.
        /// </summary>
        public required BoardWithGroupsResponse Board { get; set; }
    }
}

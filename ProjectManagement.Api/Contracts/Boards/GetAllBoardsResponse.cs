using ProjectManagement.Application.Responses.Board;

namespace ProjectManagement.Api.Contracts.Boards
{
    public sealed record GetAllBoardsResponse
    {
        public required int Page { get; init; }

        public required int Items { get; init; }

        /// <summary>
        /// Gets or sets projects.
        /// </summary>
        public required List<BoardResponse> Boards { get; set; }
    }
}

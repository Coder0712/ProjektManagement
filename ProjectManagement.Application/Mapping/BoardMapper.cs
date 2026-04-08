using ProjectManagement.Application.Responses.Card;
using ProjectManagement.Application.Responses.Group;
using ProjectManagement.Application.Responses.Board;
using ProjectManagement.Domain.Boards;
using ProjectManagement.Domain.Dtos.Queries;

namespace ProjectManagement.Application.Mapping
{
    /// <summary>
    /// Represents the board mapper.
    /// </summary>
    internal static class BoardMapper
    {
        /// <summary>
        /// Maps the board to board response.
        /// </summary>
        /// <param name="board">The board entity.</param>
        /// <returns>A board response.</returns>
        public static BoardResponse MapBoardToBoardResponse(Board board)
            => new()
            {
                Id = board.Id,
                Title = board.Title,
                ProjectId = board.ProjectId,
                CreatedAt = board.CreatedAt,
                LastModifiedAt = board.LastModifiedAt
            };

        /// <summary>
        /// Maps the board to board response.
        /// </summary>
        /// <param name="board">The board query result.</param>
        /// <returns>A board response.</returns>
        public static BoardResponse MapToBoardResponse(BoardQueryResult boardQueryResult)
            => new()
            {
                Id = boardQueryResult.Id,
                Title = boardQueryResult.Title,
                ProjectId = boardQueryResult.ProjectId,
                CreatedAt = boardQueryResult.CreatedAt,
                LastModifiedAt = boardQueryResult.LastModifiedAt
            };

        /// <summary>
        /// Maps the board to board with groups response.
        /// </summary>
        /// <param name="board">The board entity.</param>
        /// <returns>A board with groups response.</returns>
        public static BoardWithGroupsResponse MapToBoardWithGroupResponse(Board board)
            => new()
            {
                Id = board.Id,
                Title = board.Title,
                ProjectId = board.ProjectId,
                CreatedAt = board.CreatedAt,
                LastModifiedAt = board.LastModifiedAt,
                Groups = [..board.Groups.Select(g => new GroupResponse
                {
                    Id = g.Id,
                    Title = g.Title,
                    BoardId = g.BoardId,
                    CreatedAt = g.CreatedAt,
                    LastModifiedAt = g.LastModifiedAt
                })]
            };

        public static BoardWithGroupsAndCardsResponse MapToResponse(BoardQueryResult boardResult, List<CardQueryResult> cards)
            =>  new BoardWithGroupsAndCardsResponse
            {
                Id = boardResult.Id,
                Title = boardResult.Title,
                ProjectId = boardResult.ProjectId,
                CreatedAt = boardResult.CreatedAt,
                LastModifiedAt = boardResult.LastModifiedAt,
                Groups = boardResult.Groups.Select(g => new GroupWithCardsResponse
                {
                    Id = g.Id,
                    Title = g.Title,
                    BoardId = g.BoardId,
                    CreatedAt = g.CreatedAt,
                    LastModifiedAt = g.LastModifiedAt,
                    Cards = cards.Where(c => c.GroupId == g.Id).Select(c => new CardResponse
                    {
                        Id = c.Id,
                        Title = c.Title,
                        Description = c.Description,
                        Effort = c.Effort,
                        Status = c.Status,
                        GroupId = c.GroupId,
                        CreatedAt = c.CreatedAt,
                        LastModifiedAt = c.LastModifiedAt
                    }).ToList()
                }).ToList()
            };
    }
}


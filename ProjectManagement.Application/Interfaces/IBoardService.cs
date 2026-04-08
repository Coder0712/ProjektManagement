using FluentResults;
using ProjectManagement.Application.Responses.Board;

namespace ProjectManagement.Application.Interfaces
{
    public interface IBoardService
    {
        /// <summary>
        /// Creates a board.
        /// </summary>
        /// <param name="name">The name of the board.</param>
        /// <param name="projectId">The project id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>A new board.</returns>
        Task<Result<BoardResponse>> CreateBoardAsync(
            string name,
            Guid projectId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing board.
        /// </summary>
        /// <param name="id">The id of the current board.</param>
        /// <param name="name">The new name of the board.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>A board with a new name.</returns>
        Task<Result<BoardResponse>> UpdateBoardAsync(
            Guid id,
            string? name,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a board.
        /// </summary>
        /// <param name="id">The id of the board.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        Task<Result> DeleteBoardAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Result<BoardPagingResponse>> GetAllBoardsAsync(int page, int items);

        /// <summary>
        /// Gets a board by its id.
        /// </summary>
        /// <param name="id">The id of the board.</param>
        /// <returns>A single board.</returns>
        Task<Result<BoardWithGroupsAndCardsResponse>> GetBoardByIdAsync(Guid id);

        /// <summary>
        /// Creates a group.
        /// </summary>
        /// <param name="title">The title for the group.</param>
        /// <param name="boardId">The board id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>The new group.</returns>
        Task<Result<BoardWithGroupsResponse>> CreateGroupAsync(
            string title,
            Guid boardId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a group.
        /// </summary>
        /// <param name="boardId">The board id.</param>
        /// <param name="groupId">The group id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>No content.</returns>
        Task<Result> DeleteGroupAsync(
            Guid boardId,
            Guid groupId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a existing group.
        /// </summary>
        /// <param name="boardId">The board id.</param>
        /// <param name="groupId">The group id.</param>
        /// <param name="title">The new title.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>No content.</returns>
        Task<Result<BoardWithGroupsResponse>> UpdateGroupAsync(
            Guid boardId,
            Guid groupId,
            string currentTitle,
            string title,
            CancellationToken cancellationToken = default);
    }
}

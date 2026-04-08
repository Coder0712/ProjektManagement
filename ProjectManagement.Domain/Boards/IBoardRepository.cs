using ProjectManagement.Domain.Common;
using ProjectManagement.Domain.Dtos.Queries;

namespace ProjectManagement.Domain.Boards
{
    /// <summary>
    /// Represents a default implementation for the board repository.
    /// </summary>
    public interface IBoardRepository
        : IRepository<Board>
    {
        /// <summary>
        /// Gets the board entity.
        /// </summary>
        /// <param name="boardId">The board id.</param>
        /// <returns>The board entity.</returns>
        Task<Board?> GetBoardByIdAsync(Guid boardId);

        /// <summary>
        /// Get all boards.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="items">The items per page.</param>
        /// <returns>A list with board dtos.</returns>
        Task<List<BoardQueryResult>> GetAllBoardsAsync(int page, int items);

        /// <summary>
        /// Gets the a board by its id.
        /// </summary>
        /// <param name="boardId">The board id.</param>
        /// <returns>A board dto.</returns>
        Task<BoardQueryResult?> GetByIdAsync(Guid boardId);

        /// <summary>
        /// Searchs a board with the project id.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <returns>True if a board has a project.</returns>
        Task<bool> SearchBoardWithProjectAsync(Guid projectId);

        /// <summary>
        /// Gets a number of all boards.
        /// </summary>
        /// <returns>The number of boards.</returns>
        Task<int> CountAsync();

        /// <summary>
        /// Adds a group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns>No content.</returns>
        Task AddGroup(Group group);
    }
}

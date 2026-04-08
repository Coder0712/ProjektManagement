using ProjectManagement.Domain.Common;
using ProjectManagement.Domain.Dtos.Queries;

namespace ProjectManagement.Domain.Projects
{
    /// <summary>
    /// Represents a default implementation for the project repository.
    /// </summary>
    public interface IProjectRepository
        : IRepository<Project>
    {
        /// <summary>
        /// Get all projects.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="items">The items per page.</param>
        /// <returns>A list with project dtos.</returns>
        Task<List<ProjectQueryResult>> GetAllAsync(int pageSize, int items);

        /// <summary>
        /// Gets the a project by its id.
        /// </summary>
        /// <param name="boardId">The board id.</param>
        /// <returns>A project dto.</returns>
        Task<ProjectQueryResult?> GetByIdAsync(Guid projectId);

        /// <summary>
        /// Gets the project entity.
        /// </summary>
        /// <param name="boardId">The project id.</param>
        /// <returns>The project entity.</returns>
        Task<Project?> GetProjectByIdAsync(Guid projectId);

        /// <summary>
        /// Checks the existence of a project with the same name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>True if the project exists.</returns>
        Task<bool> CheckExistenceAsync(string name);

        /// <summary>
        /// Gets a number of all projects.
        /// </summary>
        /// <returns>The number of projects.</returns>
        Task<int> CountAsync();
    }
}

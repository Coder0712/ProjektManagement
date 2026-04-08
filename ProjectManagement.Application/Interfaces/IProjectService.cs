using FluentResults;
using ProjectManagement.Application.Responses.Project;
using ProjectManagement.Domain.Projects;

namespace ProjectManagement.Application.Interfaces
{
    /// <summary>
    /// A default implementation of a project service.
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="name">The name of the project.</param>
        /// <param name="description">The description of a project.</param>
        /// <param name="status">The status of the project.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>A new project.</returns>
        Task<Result<ProjectResponse>> CreateProjectAsync(
            string name,
            string description,
            ProjectStatus status,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing project.
        /// </summary>
        /// <param name="id">The id of the project.</param>
        /// <param name="name">The name of the project.</param>
        /// <param name="status">The status of the project.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>The existing project with other name or other status.</returns>
        Task<Result<ProjectResponse>> UpdateProjectAsync(
            Guid id,
            string? name,
            string? description,
            ProjectStatus? status,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a project.
        /// </summary>
        /// <param name="id">The id of the project.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        Task<Result> DeleteProjectAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all projects.
        /// </summary>
        /// <returns>A list with all projects.</returns>
        Task<Result<ProjectPagingResponse>> GetAllProjectsAsync(int page, int items);

        /// <summary>
        /// Gets a project by its id.
        /// </summary>
        /// <param name="id">The id of the project.</param>
        /// <returns>A single project.</returns>
        Task<Result<ProjectResponse>> GetProjectByIdAsync(Guid id);
    }
}

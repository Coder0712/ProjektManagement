using FluentResults;
using Microsoft.Extensions.Logging;
using ProjectManagement.Application.Errors;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Application.Responses.Project;
using ProjectManagement.Domain.Common;
using ProjectManagement.Domain.Dtos.Queries;
using ProjectManagement.Domain.Projects;
using ProjectManagement.Domain.Services;

namespace ProjectManagement.Application.Services
{
    /// <summary>
    /// Represents the project service.
    /// </summary>
    public sealed class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly IProjectTitleUniquenessChecker _projectTitleUniquenessChecker;
        private readonly ILogger<ProjectService> _logger;
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initialize a new object of type <see cref="ProjectService"/>.
        /// </summary>
        /// <param name="repository">The project repository.</param>
        /// <param name="projectTitleUniquenessChecker"><see cref="IProjectTitleUniquenessChecker"/>.</param>
        /// <param name="logger"><see cref="ILogger"/>.</param>
        /// <param name="dbContext">The db context.</param>
        public ProjectService(
            IProjectRepository repository,
            IProjectTitleUniquenessChecker projectTitleUniquenessChecker,
            ILogger<ProjectService> logger,
            IDbContext dbContext)
        {
            _repository = repository;
            _projectTitleUniquenessChecker = projectTitleUniquenessChecker;
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <inheritdoc/> 
        public async Task<Result<ProjectResponse>> CreateProjectAsync(
            string name,
            string description,
            ProjectStatus status,
            CancellationToken cancellationToken = default)
        {
            var projectResult = await Project.Create(_projectTitleUniquenessChecker, name, description, status);

            if (projectResult.IsFailed)
            {
                _logger.LogWarning("Failed to create project with name {ProjectName}. Errors: {Errors}",
                    name,
                    projectResult.Errors);

                return Result.Fail(projectResult.Errors);
            }

            await _repository.AddAsync(projectResult.Value);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Project with name {ProjectName} created successfully with id {ProjectId}.",
                name,
                projectResult.Value.Id);

            return Result.Ok(MapToResponse(projectResult.Value));
        }

        /// <inheritdoc/> 
        public async Task<Result<ProjectResponse>> UpdateProjectAsync(
            Guid id,
            string? name,
            string? description,
            ProjectStatus? status,
            CancellationToken cancellationToken = default)
        {
            var project = await _repository.GetProjectByIdAsync(id);

            if (project is null)
            {
                _logger.LogWarning("Failed to update project with id {ProjectId}. Project not found.", id);

                return Result.Fail(ApplicationErrors.Project.NotFound(id));
            }

            if (name is not null)
            {
                var updateResult = project.UpdateName(name);

                if (updateResult.IsFailed)
                {

                    _logger.LogWarning("Failed to update project with id {ProjectId}. Errors: {Errors}",
                        id,
                        updateResult.Errors);

                    return Result.Fail(updateResult.Errors);
                }
            }

            if (description is not null)
            {
                var updateResult = project.UpdateDescription(description);

                if (updateResult.IsFailed)
                {
                    _logger.LogWarning("Failed to update project with id {ProjectId}. Errors: {Errors}",
                        id,
                        updateResult.Errors);

                    return Result.Fail(updateResult.Errors);
                }
            }

            if (status is not null)
            {
                var updateResult = project.UpdateStatus(status);

                if (updateResult.IsFailed)
                {
                    _logger.LogWarning("Failed to update project with id {ProjectId}. Errors: {Errors}",
                        id,
                        updateResult.Errors);

                    return Result.Fail(updateResult.Errors);
                }
            }

            _repository.Update(project);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Project with id {ProjectId} updated successfully.", id);

            return Result.Ok(MapToResponse(project));
        }

        /// <inheritdoc/> 
        public async Task<Result> DeleteProjectAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var project = await _repository.GetProjectByIdAsync(id);

            if (project is null)
            {
                _logger.LogWarning("Failed to delete project with id {ProjectId}. Project not found.", id);

                return Result.Fail(ApplicationErrors.Project.NotFound(id));
            }

            _repository.Remove(project);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Project with id {ProjectId} deleted successfully.", id);

            return Result.Ok();
        }

        /// <inheritdoc/> 
        public async Task<Result<ProjectPagingResponse>> GetAllProjectsAsync(int page, int items)
        {
            var results = await _repository.GetAllAsync(page, items);

            var totalCount = await _repository.CountAsync();

            _logger.LogInformation("Retrieved projects for page {Page} with {Items} items per page.", page, items);

            return Result.Ok(new ProjectPagingResponse
            {
                Page = page,
                Items = items,
                TotalCount = totalCount,
                Projects = results.Select(MapToResponse).ToList()
            });
        }

        /// <inheritdoc/> 
        public async Task<Result<ProjectResponse>> GetProjectByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);

            if(result is null)
            {
                _logger.LogWarning("Failed to get project with id {ProjectId}. Project not found.", id);

                return Result.Fail<ProjectResponse>(ApplicationErrors.Project.NotFound(id));
            }

            return Result.Ok(MapToResponse(result));
        }

        private static ProjectResponse MapToResponse(Project project)
        {
            return new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Status = project.Status,
                CreatedAt = project.CreatedAt,
                LastModifiedAt = project.LastModifiedAt
            };
        }

        private static ProjectResponse MapToResponse(ProjectQueryResult result)
        {
            return new ProjectResponse
            {
                Id = result.Id,
                Name = result.Title,
                Description = result.Description,
                Status = result.Status,
                CreatedAt = result.CreatedAt,
                LastModifiedAt = result.LastModifiedAt
            };
        }
    }
}
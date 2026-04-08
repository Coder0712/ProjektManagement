using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Common;
using ProjectManagement.Domain.Dtos.Queries;
using ProjectManagement.Domain.Projects;

namespace ProjectManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Represents the project repository.
    /// </summary>
    internal sealed class ProjectRepository
        : Repository<Project>,
        IProjectRepository
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public ProjectRepository(IDbContext dbContext)
            : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<List<ProjectQueryResult>> GetAllAsync(int page, int items)
            => await this._dbContext.Set<Project>()
                .AsNoTracking()
                .Select(p => new ProjectQueryResult
                {
                    Id = p.Id,
                    Title = p.Name,
                    Description = p.Description,
                    Status = p.Status,
                    CreatedAt = p.CreatedAt,
                    LastModifiedAt = p.LastModifiedAt
                })
                .Skip((page - 1) * items)
                .Take(items)
                .ToListAsync();

        /// <inheritdoc />
        public async Task<ProjectQueryResult?> GetByIdAsync(Guid projectId)
            => await this._dbContext.Set<Project>()
                .AsNoTracking()
                .Where(p => p.Id == projectId)
                .Select(p => new ProjectQueryResult
                {
                    Id = p.Id,
                    Title = p.Name,
                    Description = p.Description,
                    Status = p.Status,
                    CreatedAt = p.CreatedAt,
                    LastModifiedAt = p.LastModifiedAt
                })
                .FirstOrDefaultAsync();

        /// <inheritdoc />
        public async Task<Project?> GetProjectByIdAsync(Guid projectId)
            => await this._dbContext.Set<Project>()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == projectId);

        /// <inheritdoc />
        public async Task<bool> CheckExistenceAsync(string name)
        {
            var project = await this._dbContext.Set<Project>()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Name == name);

            if (project is not null)
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public async Task<int> CountAsync()
            => await this._dbContext.Set<Project>()
                .AsNoTracking()
                .CountAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Boards;
using ProjectManagement.Domain.Common;
using ProjectManagement.Domain.Dtos.Queries;

namespace ProjectManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Represents the board repository.
    /// </summary>
    internal sealed class BoardRepository
        : Repository<Board>,
        IBoardRepository
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public BoardRepository(IDbContext dbContext)
            : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<BoardQueryResult?> GetByIdAsync(Guid boardId)
            =>  await _dbContext.Set<Board>()
                .AsNoTracking()
                .Where(b => b.Id == boardId)
                .Select(b => new BoardQueryResult
                {
                    Id = b.Id,
                    Title = b.Title,
                    ProjectId = b.ProjectId,
                    CreatedAt = b.CreatedAt,
                    LastModifiedAt = b.LastModifiedAt,
                    Groups = b.Groups.Select(g => new GroupQueryResult
                    {
                        Id = g.Id,
                        Title = g.Title,
                        BoardId = g.BoardId,
                        CreatedAt = g.CreatedAt,
                        LastModifiedAt = g.LastModifiedAt
                    }).ToList()
                })
                .FirstOrDefaultAsync();

        /// <inheritdoc />
        public async Task<List<BoardQueryResult>> GetAllBoardsAsync(int page, int items)
            => await this._dbContext.Set<Board>()
                .AsNoTracking()
                .Select(dto => new BoardQueryResult
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    ProjectId = dto.ProjectId,
                    CreatedAt = dto.CreatedAt,
                    LastModifiedAt = dto.LastModifiedAt
                })
                .Skip((page - 1) * items)
                .Take(items)
                .ToListAsync();

        /// <inheritdoc />
        public async Task<Board?> GetBoardByIdAsync(Guid boardId)
        {
            var board = await this._dbContext.Set<Board>()
                .Include(b => b.Groups)
                .FirstOrDefaultAsync(b => b.Id == boardId);

            if (board is null)
            {
                return null;
            }

            return board;
        }

        /// <inheritdoc />
        public async Task<int> CountAsync()
            => await this._dbContext.Set<Board>()
                .AsNoTracking()
                .CountAsync();

        /// <inheritdoc />
        public async Task<bool> SearchBoardWithProjectAsync(Guid projectId)
             => await this._dbContext.Set<Board>()
                .AsNoTracking()
                .AnyAsync(b => b.ProjectId == projectId);

        /// <inheritdoc />
        public async Task AddGroup(Group group)
        {
            this._dbContext.Set<Group>().Add(group);
        }
    }
}

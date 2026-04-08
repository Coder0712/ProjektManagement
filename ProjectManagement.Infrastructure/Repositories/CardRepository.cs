using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Cards;
using ProjectManagement.Domain.Common;
using ProjectManagement.Domain.Dtos.Queries;

namespace ProjectManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Represents the card repository.
    /// </summary>
    internal sealed class CardRepository 
        : Repository<Card>,
        ICardRepository
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public CardRepository(IDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<List<CardQueryResult>> GetAllAsync()
            => await _dbContext.Set<Card>()
                .AsNoTracking()
                .Select(c => new CardQueryResult
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Effort = c.Effort,
                    Status = c.Status,
                    Position = c.Position,
                    GroupId = c.GroupId,
                    CreatedAt = c.CreatedAt,
                    LastModifiedAt = c.LastModifiedAt
                })
                .ToListAsync();

        /// <inheritdoc />
        public async Task<CardQueryResult?> GetByIdAsync(Guid cardId)
            => await _dbContext.Set<Card>()
                .AsNoTracking()
                .Where(c => c.Id == cardId)
                .Select(c => new CardQueryResult
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Effort = c.Effort,
                    Status = c.Status,
                    Position = c.Position,
                    GroupId = c.GroupId,
                    CreatedAt = c.CreatedAt,
                    LastModifiedAt = c.LastModifiedAt
                })
                .FirstOrDefaultAsync();

        /// <inheritdoc />
        public async Task<List<CardQueryResult>> GetByIdsAsync(List<Guid> ids)
        {
            if (ids is null || ids.Count == 0)
            {
                return new List<CardQueryResult>();
            }

            return await _dbContext.Set<Card>()
                .AsNoTracking()
                .Where(c => ids.Contains(c.Id))
                .Select(c => new CardQueryResult
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Effort = c.Effort,
                    Status = c.Status,
                    Position = c.Position,
                    GroupId = c.GroupId,
                    CreatedAt = c.CreatedAt,
                    LastModifiedAt = c.LastModifiedAt
                })
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Card> GetCardByIdAsync(Guid cardId)
        {
            var card = await _dbContext.Set<Card>()
                .FirstOrDefaultAsync(c => c.Id == cardId);

            return card is null
                ? throw new Exception("Card not found")
                : card;
        }

        /// <inheritdoc />
        public async Task RemoveByIdAsync(Guid cardId)
        {
            var card = await _dbContext.Set<Card>()
                .FirstOrDefaultAsync(c => c.Id == cardId)
                ?? throw new Exception("Card not found");

            _dbContext.Set<Card>().Remove(card);
        }
    }
}

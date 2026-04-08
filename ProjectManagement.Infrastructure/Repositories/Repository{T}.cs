using ProjectManagement.Domain.Common;

namespace ProjectManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Represents the general repository.
    /// </summary>
    internal class Repository<T>
        : IRepository<T>
        where T : AggregateRoot
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository<T>"/> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public Repository(IDbContext dbContext)
            => this._dbContext = dbContext 
               ?? throw new ArgumentNullException(nameof(dbContext));

        /// <inheritdoc />
        public async Task AddAsync(T entity)
        {
            await this._dbContext.Set<T>().AddAsync(entity);
        }

        /// <inheritdoc />
        public void Remove(T entity)
        {
            this._dbContext.Set<T>().Remove(entity);
        }

        /// <inheritdoc />
        public void Update(T entity)
        {
            this._dbContext.Set<T>().Update(entity);
        }
    }
}

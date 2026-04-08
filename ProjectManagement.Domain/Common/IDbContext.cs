using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Boards;
using ProjectManagement.Domain.Projects;

namespace ProjectManagement.Domain.Common
{
    /// <summary>
    /// A default implementation for the db context.
    /// </summary>
    public interface IDbContext : IDisposable
    {
        DbSet<T> Set<T>()
            where T : Entity;

        /// <summary>
        /// Saves the chnages in the database.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

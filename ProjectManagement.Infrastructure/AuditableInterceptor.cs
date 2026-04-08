using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProjectManagement.Domain.Common;

namespace ProjectManagement.Infrastructure
{
    /// <summary>
    /// The auditable interceptor.
    /// </summary>
    public sealed class AuditableInterceptor : SaveChangesInterceptor
    {
        /// <summary>
        /// Updates the auditing before saving the entities.
        /// </summary>
        /// <param name="eventData">The event data.</param>
        /// <param name="result">The result.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            UpdateAuditingProperties(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// Sets the values for the entities.
        /// </summary>
        /// <param name="context">The db context.</param>
        private void UpdateAuditingProperties(DbContext? context)
        {
            if (context is null)
            {
                return;
            }

            foreach (var entity in context.ChangeTracker.Entries<IAuditable>())
            {
                switch (entity.State)
                {
                    case (EntityState.Added):
                        entity.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                        entity.Property("LastModifiedAt").CurrentValue = entity.Property("CreatedAt").CurrentValue;
                        break;
                    case (EntityState.Modified):
                        entity.Property("LastModifiedAt").CurrentValue = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

namespace ProjectManagement.Domain.Common
{
    /// <summary>
    /// Represents a default implementation of a general repository.
    /// </summary>
    /// <typeparam name="T">The aggregate root type.</typeparam>
    public interface IRepository<T>
        where T : AggregateRoot
    {
        /// <summary>
        /// Adds an new entity.
        /// </summary>
        /// <param name="entity">The new entity.</param>
        /// <returns></returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Removes an entity.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        void Remove(T entity);

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);
    }
}

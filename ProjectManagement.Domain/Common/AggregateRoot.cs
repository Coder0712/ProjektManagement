namespace ProjectManagement.Domain.Common
{
    /// <summary>
    /// The aggregate root.
    /// </summary>
    public abstract class AggregateRoot : Entity
    {
        /// <summary>
        /// For EFCore.
        /// </summary>
        protected AggregateRoot()
        {
        }

        /// <summary>
        /// Creates an new aggregate root.
        /// </summary>
        /// <param name="id">The id of the aggregate root.</param>
        protected AggregateRoot(Guid id)
            : base(id)
        {
        }
    }
}

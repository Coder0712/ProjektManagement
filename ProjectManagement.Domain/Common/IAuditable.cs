namespace ProjectManagement.Domain.Common
{
    /// <summary>
    /// A default implementation of auditable proeprties.
    /// </summary>
    public interface IAuditable
    {
        /// <summary>
        /// Gets the time where the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// Gets the time where the entity was modified.
        /// </summary>
        public DateTime LastModifiedAt { get; }
    }
}

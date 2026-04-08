namespace ProjectManagement.Domain.Common
{
    /// <summary>
    /// The entity base.
    /// </summary>
    public abstract class Entity : IEquatable<Entity>
    {
        /// <summary>
        /// For EFCore.
        /// </summary>
        protected Entity() { }

        /// <summary>
        /// Creates an new entity.
        /// </summary>
        /// <param name="id">The id for the entity.</param>
        protected Entity(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the id of the enity.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Equals two entities.
        /// </summary>
        /// <param name="obj">A second entity.</param>
        /// <returns>True if the ids of the entities are equal.</returns>
        public override bool Equals (object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            if (obj is not Entity entity)
            {
                return false;
            }

            return entity.Id == Id;
        }

        /// <summary>
        /// Equals entities.
        /// </summary>
        /// <param name="other">The second entity.</param>
        /// <returns>True if the ids of the entities are equal.</returns>
        public bool Equals(Entity? other)
        {
            if (other is null)
            {
                return false;
            }

            if (other.GetType() != GetType())
            {
                return false;
            }

            return other.Id == Id;
        }

        /// <summary>
        /// Gets the hash code of the id.
        /// </summary>
        /// <returns>The hashcode of the id.</returns>
        public override int GetHashCode()
            => Id.GetHashCode();

        /// <summary>
        /// Checks if two entities are equal.
        /// </summary>
        /// <param name="first">The first entity.</param>
        /// <param name="second">The second entity.</param>
        /// <returns>True if the entities are equal.</returns>
        public static bool operator==(Entity? first, Entity? second)
        {
            return first is not null && second is not null
                && first.Equals(second);
        }

        /// <summary>
        /// Checks if two entities are not equal.
        /// </summary>
        /// <param name="first">The first entity.</param>
        /// <param name="second">The second entity.</param>
        /// <returns>True if the entities are equal.</returns>
        public static bool operator!=(Entity? first, Entity? second)
        {
            return !(first == second);
        }
    }
}

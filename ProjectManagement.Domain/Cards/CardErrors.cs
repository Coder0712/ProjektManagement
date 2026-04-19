using FluentResults;

namespace ProjectManagement.Domain.Cards
{
    /// <summary>
    /// Represents the card errors.
    /// </summary>
    public static class CardErrors
    {
        /// <summary>
        /// Defines an error when the card title is empty.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error TitleIsEmpty()
            => new("The title is empty.");

        /// <summary>
        /// Defines an error when the card title is too long.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error TitleIsTooLong() 
            => new("The title is too long.");

        /// <summary>
        /// Defines an error when the description is empty.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error DescriptionIsEmpty()
            => new("Description is empty");

        /// <summary>
        /// Defines an error when the description is too long.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error DescriptionIsTooLong()
            => new("The description is too long.");

        /// <summary>
        /// Defines an error when the effort is less than 0.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error EffortIsInvalid()
            => new("Effort has an invalid value.");

        /// <summary>
        /// Defines an error when the position is less than 0.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error PositionIsNegative()
            => new("Position is negative"); 

        /// <summary>
        /// Defines an error when the group id is empty.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error GroupIdIsEmpty()
            => new("Group id is empty");
    }
}

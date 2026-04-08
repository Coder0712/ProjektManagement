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
        /// Defines an error when the description is empty.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error DescriptionIsEmpty()
            => new("Description is empty");

        /// <summary>
        /// Defines an error when the status is empty.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error StatusIsEmpty()
            => new("The status is empty.");

        /// <summary>
        /// Defines an error when the effort is empty.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error EffortIsEmpty()
            => new("The effort is empty.");

        /// <summary>
        /// Defines an error when the group id is empty.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error GroupIdIsEmpty()
            => new("Group id is empty");
    }
}

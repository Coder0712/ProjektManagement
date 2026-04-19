using FluentResults;
using ProjectManagement.Domain.Common.Errors;

namespace ProjectManagement.Domain.Boards.Errors
{
    /// <summary>
    /// Represents the board errors.
    /// </summary>
    public static class GroupErrors
    {
        /// <summary>
        /// Defines an error when the group was not found.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error GroupNotFound()
            => new NotFoundError("Group not found.");

        /// <summary>
        /// Defines an error when the title is empty.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error TitleIsEmpty()
            => new("Group title is empty.");

        /// <summary>
        /// Defines an error when the title is too long.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error TitleIsTooLong()
            => new("Title is too long.");

        /// <summary>
        /// Defines an error when the board id is empty.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error BoardIdIsEmpty()
            => new("Project id is empty.");

        /// <summary>
        /// Defines an error when the group title already exists.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error GroupTitleAlreadyExists()
            => new("A Group with this title already exists.");
    }
}

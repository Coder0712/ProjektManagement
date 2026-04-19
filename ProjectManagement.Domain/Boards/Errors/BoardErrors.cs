using FluentResults;
using static System.Net.WebRequestMethods;

namespace ProjectManagement.Domain.Boards.Errors
{
    /// <summary>
    /// Represents the board errors.
    /// </summary>
    public static class BoardErrors
    {
        /// <summary>
        /// Defines an error when the board title is empty.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error TitleIsEmpty()
            => new("Board title is empty.");

        /// <summary>
        /// Defines an error when the board title is too long.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error TitleIsTooLong()
            => new("Title is too long.");

        /// <summary>
        /// Defines an error when the project id is empty.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error ProjectIdIsEmpty()
            => new("Project id is empty.");

        /// <summary>
        /// Defines an error when the board already has a project.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error BoardAlreadyHasProject()
            => new("The board already has a project.");
    }
}

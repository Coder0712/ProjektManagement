using FluentResults;
using ProjectManagement.Domain.Common.Errors;

namespace ProjectManagement.Application.Errors
{
    /// <summary>
    /// Represents the application errors.
    /// </summary>
    public static partial class ApplicationErrors
    {
        /// <summary>
        /// The board errors.
        /// </summary>
        public static class Board
        {
            /// <summary>
            /// Defines an error when a board is not found.
            /// </summary>
            /// <param name="id">The id.</param>
            /// <returns><see cref="Error"/>.</returns>
            public static Error NotFound(Guid id)
                => new NotFoundError($"Board with id {id} not found.");

            /// <summary>
            /// Defines an error when a group is not found.
            /// </summary>
            /// <param name="id">The group id.</param>
            /// <returns><see cref="Error"/>.</returns>
            public static Error GroupNotFound(Guid groupId)
                => new NotFoundError($"Group with id {groupId} not found.");

            /// <summary>
            /// Defines an error when a project already has a board.
            /// </summary>
            /// <param name="id">The project id.</param>
            /// <returns><see cref="Error"/>.</returns>
            public static Error ProjectAlreadyHasBoard(Guid projectid)
                => new Error($"The project {projectid} already has a board.");
        }
    }
}

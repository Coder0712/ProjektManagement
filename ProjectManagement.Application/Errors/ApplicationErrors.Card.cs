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
        /// The card errors.
        /// </summary>
        public static class Card
        {
            /// <summary>
            /// Defines an error when a card is not found.
            /// </summary>
            /// <param name="id">The id.</param>
            /// <returns><see cref="Error"/>.</returns>
            public static Error NotFound(Guid id)
                => new NotFoundError($"Card with id {id} not found.");
        }
    }
}

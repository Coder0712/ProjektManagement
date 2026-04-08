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
        /// The project errors.
        /// </summary>
        public static class Project
        {
            /// <summary>
            /// Defines an error when a project is not found.
            /// </summary>
            /// <param name="id">The id.</param>
            /// <returns><see cref="Error"/>.</returns>
            public static Error NotFound(Guid id) 
                => new NotFoundError($"Project with id {id} not found.");
        }
    }
}

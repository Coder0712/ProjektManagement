using FluentResults;

namespace ProjectManagement.Domain.Common.Errors
{
    /// <summary>
    /// Represents a Not Found error.
    /// </summary>
    public class NotFoundError 
        : Error
    {
        /// <summary>
        /// Instantiates a new instance of the <see cref="NotFoundError"/>.
        /// </summary>
        /// <param name="message">The error message.</param>
        public NotFoundError(string message)
            : base(message)
        {
        }
    }
}

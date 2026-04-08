namespace ProjectManagement.Domain.Services
{
    /// <summary>
    /// Represents a service that checks if a project is already assigned to a board.
    /// </summary>
    public interface IBoardProjectUniquessChecker
    {
        /// <summary>
        /// Checks if a project is already assigned to a board.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <returns>True if the project is already assigned.</returns>
        Task<bool> ProjectAlreadyAssignedAsync(Guid projectId);
    }
}

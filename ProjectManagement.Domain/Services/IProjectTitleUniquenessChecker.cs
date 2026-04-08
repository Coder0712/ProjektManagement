namespace ProjectManagement.Domain.Services
{
    /// <summary>
    /// Represents a service that checks if a project has the same title.
    /// </summary>
    public interface IProjectTitleUniquenessChecker
    {
        /// <summary>
        /// Checks if a project already has the same title.
        /// </summary>
        /// <param name="title">The project title.</param>
        /// <returns>True if a project has the same title.</returns>
        Task<bool> ProjectAlreadyHasTitle(string title);
    }
}

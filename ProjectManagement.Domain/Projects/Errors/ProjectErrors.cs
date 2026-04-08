using FluentResults;

namespace ProjectManagement.Domain.Projects.Errors
{
    /// <summary>
    /// Represents the project errors.
    /// </summary>
    public static class ProjectErrors
    {
        /// <summary>
        /// Defines an error when the project has a title.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error ProjectHasTitle()
            => new("Project with the same name already exists.");

        /// <summary>
        /// Defines an error when the project title is empty.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error TitleIsEmpty()
            => new("Project name is empty.");

        /// <summary>
        /// Defines an error when the description is empty.
        /// </summary>
        /// <returns><see cref="Error"/>.</returns>
        public static Error DescriptionIsEmpty()
            => new("Description is empty.");
    }
}

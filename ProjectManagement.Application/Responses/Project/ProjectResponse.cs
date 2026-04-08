using ProjectManagement.Domain.Projects;

namespace ProjectManagement.Application.Responses.Project
{
    /// <summary>
    /// Represents the project response.
    /// </summary>
    public sealed record ProjectResponse
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        public ProjectStatus? Status { get; init; }

        /// <sum/// <summary>
        /// Gets the date and time when the object was created.
        /// </summary>
        public DateTime CreatedAt { get; init; }

        /// <summary>
        /// Gets the date and time when the entity was last modified.
        /// </summary>
        public DateTime LastModifiedAt { get; init; }
    }
}

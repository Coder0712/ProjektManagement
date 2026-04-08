using FluentResults;
using ProjectManagement.Domain.Boards;
using ProjectManagement.Domain.Common;
using ProjectManagement.Domain.Projects.Errors;
using ProjectManagement.Domain.Services;

namespace ProjectManagement.Domain.Projects
{
    /// <summary>
    /// The project aggregate.
    /// </summary>
    public sealed class Project : AggregateRoot, IAuditable
    {
        /// <summary>
        /// For EFCore.
        /// </summary>
        protected Project()
        {
        }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="status">The status.</param>
        protected Project(
            Guid id,
            string name,
            string description,
            ProjectStatus status)
            : base(id)
        {
            Name = name;
            Description = description;
            Status = status;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        public ProjectStatus? Status { get; private set; }

        public Board Board { get; init; }

        /// <inheritdoc/>
        public DateTime CreatedAt { get; init; }

        /// <inheritdoc/>
        public DateTime LastModifiedAt { get; init; }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="exist">Whether the project already exists.</param>
        /// <param name="name">The name of the project.</param>
        /// <param name="description">The description of the project.</param>
        /// <param name="status">The status of the project.</param>
        /// <returns>A new project.</returns>
        public static async Task<Result<Project>> Create(
            IProjectTitleUniquenessChecker checker,
            string name,
            string description,
            ProjectStatus status)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Fail<Project>(ProjectErrors.TitleIsEmpty());
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                return Result.Fail<Project>(ProjectErrors.DescriptionIsEmpty());
            }

            var hasTitle = await checker.ProjectAlreadyHasTitle(name);

            if (hasTitle)
            {
                return Result.Fail<Project>(ProjectErrors.ProjectHasTitle());
            }

            return Result.Ok(new Project()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Status = status
            });
        }

        /// <summary>
        /// Change the name of the project.
        /// </summary>
        /// <param name="name">The new name.</param>
        /// <returns>The updated project.</returns>
        public Result<Project> UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Fail<Project>(ProjectErrors.TitleIsEmpty());
            }

            this.Name = name;
            return this;
        }

        /// <summary>
        /// Change the description of the project.
        /// </summary>
        /// <param name="name">The new description.</param>
        /// <returns>The updated project.</returns>
        public Result<Project> UpdateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return Result.Fail<Project>(ProjectErrors.DescriptionIsEmpty());
            }

            this.Description = description;
            return this;
        }

        /// <summary>
        /// Change the status of the project.
        /// </summary>
        /// <param name="name">The new status.</param>
        /// <returns>The updated project.</returns>
        public Result<Project> UpdateStatus(ProjectStatus? status)
        {
            this.Status = status;

            return this;
        }
    }
}

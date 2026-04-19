using FluentResults;
using ProjectManagement.Domain.Boards.Errors;
using ProjectManagement.Domain.Common;
using ProjectManagement.Domain.Projects;
using ProjectManagement.Domain.Services;
using System.Threading.Tasks.Dataflow;

namespace ProjectManagement.Domain.Boards
{
    /// <summary>
    /// The board aggregate.
    /// </summary>
    public sealed class Board : AggregateRoot, IAuditable
    {
        private readonly List<Group> _groups = new List<Group>();

        /// <summary>
        /// For EfCore.
        /// </summary>
        protected Board()
        {
        }

        /// <summary>
        /// Creates a new board.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="title">The title.</param>
        /// <param name="projectId">The project id.</param>
        protected Board(
            Guid id,
            string title,
            Guid projectId)
            : base(id)
        {
            Title = title;
            ProjectId = projectId;
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the groups.
        /// </summary>
        public IReadOnlyCollection<Group> Groups => _groups;

        /// <summary>
        /// Gets the project id.
        /// </summary>
        public Guid ProjectId { get; init; }

        /// <summary>
        /// Gets the project.
        /// </summary>
        public Project Project { get; init; }

        /// <inheritdoc/>
        public DateTime CreatedAt { get; private set; }

        /// <inheritdoc/>
        public DateTime LastModifiedAt { get; private set; }

        /// <summary>
        /// Creates a new board.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="projectId">The project id.</param>
        /// <returns>The new board.</returns>
        public async static Task<Result<Board>> Create(
            IBoardProjectUniquessChecker checker,
            string title,
            Guid projectId)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result.Fail(BoardErrors.TitleIsEmpty());
            }

            if(title.Length > 255)
            {
                return Result.Fail(BoardErrors.TitleIsTooLong());
            }

            if (projectId == Guid.Empty)
            {
                return Result.Fail(BoardErrors.ProjectIdIsEmpty());
            }

            var projectHasBoard = await checker.ProjectAlreadyAssignedAsync(projectId);

            if (projectHasBoard)
            {
                return Result.Fail(BoardErrors.BoardAlreadyHasProject());
            }

            return Result.Ok(new Board()
            {
                Id = Guid.NewGuid(),
                Title = title,
                ProjectId = projectId
            });
        }

        /// <summary>
        /// Updates the title.
        /// </summary>
        /// <param name="title">The new title.</param>
        /// <returns>The board with the new title.</returns>
        public Result<Board> UpdateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result.Fail(BoardErrors.TitleIsEmpty());
            }

            if (title.Length > 255)
            {
                return Result.Fail(BoardErrors.TitleIsTooLong());
            }

            this.Title = title;

            return Result.Ok(this);
        }

        /// <summary>
        /// Creates a group.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="boardId">The board id.</param>
        /// <returns>The board with the groups.</returns>
        public Result<Group> CreateGroup(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result.Fail(GroupErrors.TitleIsEmpty());
            }

            if(this._groups.Any(g => g.Title == title))
            {
                return Result.Fail(GroupErrors.GroupTitleAlreadyExists());
            }

            var groupResult = Group.Create(title, this.Id);

            if (groupResult.IsFailed)
            {
                return Result.Fail(groupResult.Errors);
            }

            _groups.Add(groupResult.Value);

            return Result.Ok(groupResult.Value);
        }

        public Result<Board> UpdateGroup(string oldTitle, string? title)
        {
            var group = this._groups.FirstOrDefault(g => g.Title == oldTitle);

            if(group is null)
            {
                return Result.Fail(GroupErrors.GroupNotFound());
            }

            if(title is null)
            {
                return Result.Ok(this);
            }

            if (this._groups.Any(g => g.Title == title && g != group))
            {
                return Result.Fail(GroupErrors.GroupTitleAlreadyExists());
            }

            group.UpdateTitle(title);

            return Result.Ok(this);
        }

        /// <summary>
        /// Removes a group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns>The board with the groups.</returns>
        /// <exception cref="Exception">If the group has cards.</exception>
        public Result<Board> RemoveGroup(Group group)
        {
            if (!_groups.Contains(group))
            {
                return Result.Fail(GroupErrors.GroupNotFound());
            }

            this._groups.Remove(group);

            return Result.Ok(this);
        }
    }
}

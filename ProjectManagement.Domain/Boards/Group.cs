using FluentResults;
using ProjectManagement.Domain.Boards.Errors;
using ProjectManagement.Domain.Common;

namespace ProjectManagement.Domain.Boards
{
    /// <summary>
    /// Represents the group entity.
    /// </summary>
    public sealed class Group : Entity, IAuditable
    {
        /// <summary>
        /// For EFCore.
        /// </summary>
        protected Group()
        {
        }

        /// <summary>
        /// Creates a new group.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="title">The title.</param>
        /// <param name="boardId">The board id.</param>
        protected Group(
            Guid id,
            string title,
            Guid boardId)
            : base(id)
        {
            BoardId = boardId;
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the board.
        /// </summary>
        public Board Board { get; init; }

        /// <summary>
        /// Gets the board id.
        /// </summary>
        public Guid BoardId { get; init; }

        /// <inheritdoc/>
        public DateTime CreatedAt { get; private set; }

        /// <inheritdoc/>
        public DateTime LastModifiedAt { get; private set; }

        /// <summary>
        /// Creates a new group.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="boardId">The board id.</param>
        /// <returns>A new group.</returns>
        public static Result<Group> Create(
            string title,
            Guid boardId)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result.Fail(GroupErrors.TitleIsEmpty());
            }

            if(boardId == Guid.Empty)
            {
                return Result.Fail(GroupErrors.BoardIdIsEmpty());
            }

            return Result.Ok(new Group()
            {
                Id = Guid.NewGuid(),
                Title = title,
                BoardId = boardId
            });
        }

        /// <summary>
        /// Updates the title.
        /// </summary>
        /// <param name="title">The new title.</param>
        /// <returns>The updated group.</returns>
        public Result<Group> UpdateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result.Fail(GroupErrors.TitleIsEmpty());
            }

            this.Title = title;

            return Result.Ok(this);
        }
    }
}
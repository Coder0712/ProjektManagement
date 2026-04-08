using FluentResults;
using ProjectManagement.Domain.Common;

namespace ProjectManagement.Domain.Cards
{
    /// <summary>
    /// Represents the card entity.
    /// </summary>
    public sealed class Card : AggregateRoot, IAuditable
    {
        /// <summary>
        /// Fore EFCore.
        /// </summary>
        protected Card()
        {
        }

        /// <summary>
        /// Creates a new card.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="effort">The effort.</param>
        /// <param name="status">The status.</param>
        /// <param name="groupId">The group id.</param>
        protected Card(
            Guid id,
            string title,
            string? description,
            int? effort,
            CardStatus status,
            Guid groupId)
            : base(id)
        {
            this.Title = title;
            this.Description = description;
            this.Effort = effort;
            this.Status = status;
            this.GroupId = groupId;
        }

        /// <summary>
        /// The title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// The description.
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// The effort.
        /// </summary>
        public int? Effort { get; private set; }

        /// <summary>
        /// The status.
        /// </summary>
        public CardStatus? Status { get; private set; }

        public int Position { get; private set; }

        /// <summary>
        /// The group id.
        /// </summary>
        public Guid GroupId { get; private set; }

        /// <inheritdoc/>
        public DateTime CreatedAt { get; private set; }

        /// <inheritdoc/>
        public DateTime LastModifiedAt { get; private set; }

        /// <summary>
        /// Creates a new card.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="effort">The effort.</param>
        /// <param name="status">The status.</param>
        /// <param name="groupId">The group id.</param>
        /// <returns>A new card.</returns>
        public static Result<Card> Create(
            string title,
            string? description,
            int? effort,
            CardStatus status,
            int position,
            Guid groupId)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result.Fail(CardErrors.TitleIsEmpty());
            }

            return Result.Ok(new Card()
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                Effort = effort,
                Status = status,
                Position = position,
                GroupId = groupId
            });
        }

        /// <summary>
        /// Updates the title of the card.
        /// </summary>
        /// <param name="title">The new title.</param>
        /// <returns>The updated card instance.</returns>
        public Result<Card> UpdateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result.Fail(CardErrors.TitleIsEmpty());
            }

            this.Title = title;

            return Result.Ok(this);
        }

        /// <summary>
        /// Updates the description of the card.
        /// </summary>
        public Result<Card> UpdateDescription(string description)
        {
            if (description is null)
            {
                return Result.Fail(CardErrors.DescriptionIsEmpty());
            }

            this.Description = description;
            return this;
        }

        /// <summary>
        /// Updates the effort of the card.
        /// </summary>
        public Result<Card> UpdateEffort(int effort)
        {
            this.Effort = effort;

            return Result.Ok(this);
        }

        /// <summary>
        /// Updates the status of the card.
        /// </summary>
        public Result<Card> UpdateStatus(CardStatus? status)
        {
            this.Status = status;

            return Result.Ok(this);
        }

        /// <summary>
        /// Moves the card.
        /// </summary>
        /// <param name="newGroupId">The new group id.</param>
        /// <param name="newPosition">The new position.</param>
        /// <returns></returns>
        public Result MoveCard(Guid newGroupId, int newPosition)
        {
            if (newGroupId == Guid.Empty)
            {
                return Result.Fail(CardErrors.GroupIdIsEmpty());
            }

            this.GroupId = newGroupId;

            this.Position = newPosition;

            return Result.Ok();
        }
    }
}

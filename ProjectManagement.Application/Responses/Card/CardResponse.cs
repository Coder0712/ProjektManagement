using ProjectManagement.Domain.Cards;

namespace ProjectManagement.Application.Responses.Card
{
    /// <summary>
    /// Represents the card response.
    /// </summary>
    public sealed record CardResponse
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title { get; init; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// Gets the effort.
        /// </summary>
        public int? Effort { get; init; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        public CardStatus? Status { get; init; }

        /// <summary>
        /// Gets the group id.
        /// </summary>
        public Guid GroupId { get; init; }

        /// <summary>
        /// Gets the date and time when the object was created.
        /// </summary>
        public DateTime CreatedAt { get; init; }

        /// <summary>
        /// Gets the date and time when the entity was last modified.
        /// </summary>
        public DateTime LastModifiedAt { get; init; }
    }
}

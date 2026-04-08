using ProjectManagement.Application.Responses.Card;

namespace ProjectManagement.Application.Responses.Group
{
    /// <summary>
    /// Represents the groups with cards response.
    /// </summary>
    public sealed record GroupWithCardsResponse
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
        /// Gets the board id.
        /// </summary>
        public Guid BoardId { get; init; }

        /// <summary>
        /// Gets the cards.
        /// </summary>
        public List<CardResponse> Cards { get; init; } = new();

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

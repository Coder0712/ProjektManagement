using ProjectManagement.Application.Responses.Group;

namespace ProjectManagement.Application.Responses.Board
{
    /// <summary>
    /// Gets the board with groups and cards response.
    /// </summary>
    public sealed record BoardWithGroupsAndCardsResponse
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
        /// Gets the project id.
        /// </summary>
        public Guid ProjectId { get; init; }

        /// <summary>
        /// Gets the groups with cards.
        /// </summary>
        public List<GroupWithCardsResponse> Groups { get; init; } = new();

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

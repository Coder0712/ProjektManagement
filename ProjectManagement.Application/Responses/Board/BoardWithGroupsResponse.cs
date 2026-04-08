using ProjectManagement.Application.Responses.Group;

namespace ProjectManagement.Application.Responses.Board
{
    /// <summary>
    /// Represents the board with groups response.
    /// </summary>
    public sealed record BoardWithGroupsResponse
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
        /// Gets the groups.
        /// </summary>
        public List<GroupResponse> Groups { get; init; } = new();

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

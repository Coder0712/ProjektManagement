namespace ProjectManagement.Application.Responses.Board
{
    /// <summary>
    /// Represents the board response.
    /// </summary>
    public sealed record BoardResponse
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Gets the title of the board.
        /// </summary>
        public string Title { get; init; }

        /// <summary>
        /// The project id.
        /// </summary>
        public Guid ProjectId { get; init; }

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

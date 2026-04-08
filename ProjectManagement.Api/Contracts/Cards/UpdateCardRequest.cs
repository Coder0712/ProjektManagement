namespace ProjectManagement.Api.Contracts.Cards
{
    /// <summary>
    /// Represents a request to update a card
    /// </summary>
    public sealed record UpdateCardRequest
    {
        /// <summary>
        /// Gets the title.
        /// </summary>
        public string? Title { get; init; }

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
        public string? Status { get; init; }

        /// <summary>
        /// Gets the board id.
        /// </summary>
        public required string BoardId { get; init; }
    }
}

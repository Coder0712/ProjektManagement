namespace ProjectManagement.Api.Contracts.Cards
{
    /// <summary>
    /// Represents a request to move a card to a different group within a board.
    /// </summary>
    public sealed record MoveCardRequest
    {
        /// <summary>
        /// Gets the board id.
        /// </summary>
        public required string BoardId { get; init; }

        /// <summary>
        /// Gets the new group id.
        /// </summary>
        public required string NewGroupId { get; init; }
    }
}

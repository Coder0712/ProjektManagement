namespace ProjectManagement.Api.Contracts.Boards
{
    /// <summary>
    /// Represents a request to update a board.
    /// </summary>
    public sealed record UpdateBoardRequst
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string? Title { get; set; }
    }
}

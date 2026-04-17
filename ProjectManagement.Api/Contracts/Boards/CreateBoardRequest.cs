namespace ProjectManagement.Api.Contracts.Boards
{
    /// <summary>
    /// Represents a request to create a board
    /// </summary>
    public sealed record CreateBoardRequest
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public required string Title { get; set; }
        
        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        public required string ProjectId { get; set; }
    }
}

namespace ProjectManagement.Api.Contracts.Groups
{
    /// <summary>
    /// Represents a request for updating a group.
    /// </summary>
    public sealed record UpdateGroupRequest
    {
        /// <summary>
        /// Gets the current title.
        /// </summary>
        public required string CurrentTitle { get; init; }

        /// <summary>
        /// Gets the new title.
        /// </summary>
        public string? NewTitle { get; init; }
    }
}

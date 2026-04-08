namespace ProjectManagement.Api.Contracts.Groups
{
    /// <summary>
    /// Represents a request to add a new group.
    /// </summary>
    public class CreateGroupRequest
    {
        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title { get; init; }
    }
}

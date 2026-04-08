using System.ComponentModel;

namespace ProjectManagement.Api.Contracts.Cards
{
    /// <summary>
    /// Represents a request to create a card
    /// </summary>
    public sealed record CreateCardRequest
    {
        /// <summary>
        /// Gets the title.
        /// </summary>
        public required string Title { get; init; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// Gets the effort.
        /// </summary>
        [DefaultValue(0)]
        public int? Effort { get; init; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        public required string Status { get; init; }

        /// <summary>
        /// Gets the board id.
        /// </summary>
        public required string BoardId { get; init; }

        /// <summary>
        /// Gets the group id.
        /// </summary>
        public required string GroupId { get; init; }
    }
}

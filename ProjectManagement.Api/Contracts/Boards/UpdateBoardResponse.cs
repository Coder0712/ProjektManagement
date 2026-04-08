using ProjectManagement.Domain.Boards;

namespace ProjectManagement.Api.Contracts.Boards
{
    /// <summary>
    /// Represents the response to update a kanban board.
    /// </summary>
    public sealed record UpdateBoardResponse
    {
        /// <summary>
        /// Gets or sets the kanban board.
        /// </summary>
        public required Board KanbanBoard { get; set; }
    }
}

using ProjectManagement.Domain.Common;
using ProjectManagement.Domain.Dtos.Queries;

namespace ProjectManagement.Domain.Cards
{
    /// <summary>
    /// Represents a default implementation for the card repository.
    /// </summary>
    public interface ICardRepository : IRepository<Card>
    {
        /// <summary>
        /// Get all cards.
        /// </summary>
        /// <returns>A list with card dtos.</returns>
        Task<List<CardQueryResult>> GetAllAsync();

        /// <summary>
        /// Gets the card by its id.
        /// </summary>
        /// <param name="cardId">The card id.</param>
        /// <returns>The card dto.</returns>
        Task<CardQueryResult?> GetByIdAsync(Guid cardId);

        /// <summary>
        /// Get cards by their ids.
        /// </summary>
        /// <param name="ids">A list with card ids.</param>
        /// <returns></returns>
        Task<List<CardQueryResult>> GetByIdsAsync(List<Guid> ids);

        /// <summary>
        /// Gets the card entity.
        /// </summary>
        /// <param name="boardId">The card id.</param>
        /// <returns>The card entity.</returns>
        Task<Card> GetCardByIdAsync(Guid cardId);

        /// <summary>
        /// Remove a card by its id.
        /// </summary>
        /// <param name="cardId">The card id.</param>
        /// <returns></returns>
        Task RemoveByIdAsync(Guid cardId);
    }
}

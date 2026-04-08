using FluentResults;
using ProjectManagement.Application.Responses.Card;
using ProjectManagement.Domain.Cards;

namespace ProjectManagement.Application.Interfaces
{
    /// <summary>
    /// A default implementation of a card service.
    /// </summary>
    public interface ICardsService
    {
        /// <summary>
        /// Creates a new card.
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="description">The description.</param>
        /// <param name="effort">The effort.</param>
        /// <param name="status">The status.</param>
        /// <param name="boardId">The board id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>A new instance of type <see cref="Cards"/>.</returns>
        Task<Result<CardResponse>> Create(
            string title,
            string? description,
            int? effort,
            CardStatus status,
            Guid groupId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a card.
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="description">The description.</param>
        /// <param name="effort">The effort.</param>
        /// <param name="status">The status.</param>
        /// <param name="boardId">The board id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>A updated instance of type <see cref="Cards"/>.</returns>
        Task<Result<CardResponse>> UpdateCard(
            Guid cardId,
            string? title,
            string? description,
            int? effort,
            CardStatus? status,
            Guid boardId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a card.
        /// </summary>
        /// <param name="cardId">The card id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        Task<Result> DeleteCard(Guid cardId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all cards.
        /// </summary>
        /// <returns>A list with all cards.</returns>
        Task<Result<List<CardResponse>>> GetAllCardsAsync();

        /// <summary>
        /// Gets a card by the id.
        /// </summary>
        /// <param name="id">The id of the card.</param>
        /// <returns>Gets a card.</returns>
        Task<Result<CardResponse>> GetCardbyIdAsync(Guid id);

        /// <summary>
        /// Moves a card.
        /// </summary>
        /// <param name="boardId">The board id.</param>
        /// <param name="newGroupId">The new group id.</param>
        /// <param name="cardId">The card id.</param>
        /// <returns>No content.</returns>
        Task<Result> MoveCard(Guid boardId, Guid newGroupId, Guid cardId);
    }
}

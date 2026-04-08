using FluentResults;
using Microsoft.Extensions.Logging;
using ProjectManagement.Application.Errors;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Application.Responses.Card;
using ProjectManagement.Domain.Cards;
using ProjectManagement.Domain.Common;
using ProjectManagement.Domain.Dtos.Queries;

namespace ProjectManagement.Application.Services
{
    /// <summary>
    /// Represents the card service.
    /// </summary>
    public sealed class CardService : ICardsService
    {
        private readonly ICardRepository _cardRepository;
        private readonly ILogger<CardService> _logger;
        private readonly IDbContext _dbContext;

        public CardService(
            ICardRepository cardRepository,
            ILogger<CardService> logger,
            IDbContext dbContext)
        {
            _cardRepository = cardRepository;
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Result<CardResponse>> Create(
            string title,
            string? description,
            int? effort,
            CardStatus status,
            Guid groupId,
            CancellationToken cancellationToken = default)
        {
            var cards = await _cardRepository.GetAllAsync();

            var position = cards.Count(c => c.GroupId == groupId);

            var cardResult = Card.Create(title, description, effort, status, position, groupId);

            if (cardResult.IsFailed)
            {
                _logger.LogWarning("Failed to create card with title {CardTitle}. Errors: {Errors}",
                    title,
                    cardResult.Errors);

                return Result.Fail(cardResult.Errors);
            }

            await _cardRepository.AddAsync(cardResult.Value);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Card with id {CardId} created successfully.", cardResult.Value.Id);

            return MapToResponse(cardResult.Value);
        }

        public async Task<Result<CardResponse>> UpdateCard(
            Guid cardId,
            string? title,
            string? description,
            int? effort,
            CardStatus? status,
            Guid boardId,
            CancellationToken cancellationToken = default)
        {
            var card = await _cardRepository.GetCardByIdAsync(cardId);

            if (card is null)
            {
                _logger.LogWarning("Failed to update card with id {CardId}. Card not found.", cardId);

                return Result.Fail(ApplicationErrors.Card.NotFound(cardId));
            }

            if (title is not null)
            {
                var result = card.UpdateTitle(title);

                if (result.IsFailed)
                {
                    _logger.LogWarning("Failed to update title for card with id {CardId}. Errors: {Errors}",
                        cardId,
                        result.Errors);

                    return Result.Fail(result.Errors);
                }
            }

            if (description is not null)
            {
                var result = card.UpdateDescription(description);
                if (result.IsFailed)
                {
                    _logger.LogWarning("Failed to update description for card with id {CardId}. Errors: {Errors}",
                        cardId,
                        result.Errors);

                    return Result.Fail(result.Errors);
                }
            }

            if (effort is not null)
            {
                var result = card.UpdateEffort(effort.Value);

                if (result.IsFailed)
                {
                    _logger.LogWarning("Failed to update effort for card with id {CardId}. Errors: {Errors}",
                        cardId,
                        result.Errors);

                    return Result.Fail(result.Errors);
                }
            }

            if (status is not null)
            {
                var result = card.UpdateStatus(status);

                if (result.IsFailed)
                {
                    _logger.LogWarning("Failed to update status for card with id {CardId}. Errors: {Errors}",
                        cardId,
                        result.Errors);

                    return Result.Fail(result.Errors);
                }
            }

            _cardRepository.Update(card);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Card with id {CardId} updated successfully.", cardId);

            return Result.Ok(MapToResponse(card));
        }

        public async Task<Result> DeleteCard(Guid cardId, CancellationToken cancellationToken = default)
        {
            var card = await _cardRepository.GetCardByIdAsync(cardId);

            if (card is null)
            {
                _logger.LogWarning("Failed to delete card with id {CardId}. Card not found.", cardId);

                return Result.Fail(ApplicationErrors.Card.NotFound(cardId));
            }

            _cardRepository.Remove(card);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Card with id {CardId} deleted successfully.", cardId);

            return Result.Ok();
        }

        public async Task<Result<List<CardResponse>>> GetAllCardsAsync()
        {
            var cards = await _cardRepository.GetAllAsync();

            var cardQueryResults = cards.Select(c => new CardQueryResult
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Effort = c.Effort,
                Status = c.Status,
                GroupId = c.GroupId,
                CreatedAt = c.CreatedAt,
                LastModifiedAt = c.LastModifiedAt
            }).ToList();

            _logger.LogInformation("Retrieved all cards. Total count: {CardCount}.", cardQueryResults.Count);

            return Result.Ok(cardQueryResults.Select(MapToResponse).ToList());
        }

        public async Task<Result<CardResponse>> GetCardbyIdAsync(Guid id)
        {
            var result = await _cardRepository.GetByIdAsync(id);

            if (result is null)
            {
                _logger.LogWarning("Failed to retrieve card with id {CardId}. Card not found.", id);

                return Result.Fail(ApplicationErrors.Card.NotFound(id));
            }

            _logger.LogInformation("Card with id {CardId} retrieved successfully.", id);

            return Result.Ok(MapToResponse(result));
        }

        public async Task<Result> MoveCard(Guid boardId, Guid newGroupId, Guid cardId)
        {
            var cards = await _cardRepository.GetAllAsync();

            var position = cards.Count(c => c.GroupId == newGroupId);

            var card = await _cardRepository.GetCardByIdAsync(cardId);

            if (card is null)
            {
                _logger.LogWarning("Failed to move card with id {CardId}. Card not found.", cardId);

                return Result.Fail(ApplicationErrors.Card.NotFound(cardId));
            }

            var moveResult = card.MoveCard(newGroupId, position);

            if (moveResult.IsFailed)
            {
                _logger.LogWarning("Failed to move card with id {CardId} to group {GroupId}. Errors: {Errors}",
                    cardId,
                    newGroupId,
                    moveResult.Errors);

                return Result.Fail(moveResult.Errors);
            }

            _cardRepository.Update(card);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Card with id {CardId} moved to group {GroupId} successfully.",
                cardId,
                newGroupId);

            return Result.Ok();
        }

        private static CardResponse MapToResponse(Card card)
        {
            return new CardResponse
            {
                Id = card.Id,
                Title = card.Title,
                Description = card.Description,
                Effort = card.Effort,
                Status = card.Status,
                GroupId = card.GroupId,
                CreatedAt = card.CreatedAt,
                LastModifiedAt = card.LastModifiedAt
            };
        }

        private static CardResponse MapToResponse(CardQueryResult result)
        {
            return new CardResponse
            {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description,
                Effort = result.Effort,
                Status = result.Status,
                GroupId = result.GroupId,
                CreatedAt = result.CreatedAt,
                LastModifiedAt = result.LastModifiedAt
            };
        }
    }
}

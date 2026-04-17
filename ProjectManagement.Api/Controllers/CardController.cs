using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Contracts.Cards;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Domain.Cards;
using ProjectManagement.Domain.Common.Errors;

namespace ProjectManagement.Controllers
{
    /// <summary>
    /// Controller for the card endpoints.
    /// </summary>
    [Route("")]
    [Authorize(Roles = "user")]
    [ApiController]
    public sealed class CardController : ControllerBase
    {
        private readonly ICardsService _cardService;
        private readonly IValidator<CreateCardRequest> _createValidator;
        private readonly IValidator<UpdateCardRequest> _updateValidator;
        private readonly IValidator<MoveCardRequest> _moveValidator;

        public CardController(
            ICardsService cardService,
            IValidator<CreateCardRequest> createValidator,
            IValidator<UpdateCardRequest> updateValidator,
            IValidator<MoveCardRequest> moveValidator)
        {
            _cardService = cardService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _moveValidator = moveValidator;
        }

        /// <summary>
        /// Creates a new card.
        /// </summary>
        /// <param name="request"><see cref="CreateCardRequest"/>.</param>
        /// <returns>A new card.</returns>
        [Route("/api/project-management/cards")]
        [HttpPost]
        [ProducesResponseType(typeof(CreateCardResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCard([FromBody] CreateCardRequest request)
        {
            var result = await _createValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                return Problem(
                    result.Errors.First().ErrorMessage,
                    null,
                    StatusCodes.Status400BadRequest,
                    "Card could not be created.");
            }

            var cardStatus = Enum.Parse<CardStatus>(request.Status.ToString(), true);

            var cardResult = await _cardService.Create(
            request.Title,
            request.Description,
            request.Effort,
            cardStatus,
            new Guid(request.GroupId));

            if (cardResult.IsFailed)
            {
                return Problem(
                    cardResult.Errors.First().Message,
                    null,
                    StatusCodes.Status400BadRequest,
                    "Card could not be created.");
            }

            return CreatedAtAction("GetCardById", new { id = cardResult.Value.Id }, cardResult.Value);
        }

        /// <summary>
        /// Gets all cards.
        /// </summary>
        /// <returns>A list with all cards.</returns>
        [Route("/api/project-management/cards")]
        [HttpGet]
        [ProducesResponseType(typeof(GetAllCardsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCards()
        {
            var allCards = await _cardService.GetAllCardsAsync();

            return Ok(new GetAllCardsResponse { Cards = allCards.Value });
        }

        /// <summary>
        /// Gets a card by the id.
        /// </summary>
        /// <param name="id">The id of the card.</param>
        /// <returns>A single card.</returns>
        [Route("/api/project-management/cards/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(GetCardByIdResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCardById([FromRoute] Guid id)
        {
            var cardResult = await _cardService.GetCardbyIdAsync(id);

            if (cardResult.IsFailed)
            {
                return Problem(
                    cardResult.Errors.First().Message,
                    null,
                    StatusCodes.Status404NotFound,
                    "Card could not be found.");
            }

            return Ok(cardResult.Value);
        }

        /// <summary>
        /// Updates a card.
        /// </summary>
        /// <param name="id">The id of the card.</param>
        /// <param name="request"><see cref="UpdateCardRequest"/>.</param>
        /// <returns>An updated card.</returns>
        [Route("/api/project-management/cards/{id}")]
        [HttpPatch]
        [ProducesResponseType(typeof(UpdateCardResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCard(
            [FromRoute] Guid id,
            [FromBody] UpdateCardRequest request)
        {
            var result = await _updateValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                return Problem(
                    result.Errors.First().ErrorMessage,
                    null,
                    StatusCodes.Status400BadRequest,
                    "Card could not be updated.");
            }

            CardStatus cardStatus = default;

            if (!string.IsNullOrWhiteSpace(request.Status))
            {
                cardStatus = Enum.Parse<CardStatus>(request.Status.ToString(), true);
            }
            
            var cardResult = await _cardService.UpdateCard(
                id,
                request.Title,
                request.Description,
                request.Effort,
                cardStatus,
                new Guid(request.BoardId));

            if (cardResult.IsFailed)
            {
                if (cardResult.Errors.OfType<NotFoundError>().Any())
                {
                    return Problem(
                        cardResult.Errors.First().Message,
                        null,
                        StatusCodes.Status404NotFound,
                        "Card could not be updated.");
                }
                else
                {
                    return Problem(
                        cardResult.Errors.First().Message,
                        null,
                        StatusCodes.Status400BadRequest,
                        "Card could not be updated.");
                }
            }

            return Ok(cardResult.Value);
        }

        /// <summary>
        /// Deletes a card by the id.
        /// </summary>
        /// <param name="id">The id of the card.</param>
        /// <returns>No content.</returns>
        [Route("/api/project-management/cards/{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCardById([FromRoute] Guid id)
        {
            var cardResult = await _cardService.DeleteCard(id);

            if (cardResult.IsFailed)
            {
                return Problem(
                    cardResult.Errors.First().Message,
                    null,
                    StatusCodes.Status404NotFound,
                    "Card could not be deleted.");
            }

            return NoContent();
        }

        /// <summary>
        /// Moves the card.
        /// </summary>
        /// <param name="id">The card id.</param>
        /// <param name="request"><see cref="MoveCardRequest"/>.</param>
        /// <returns></returns>
        [Route("/api/project-management/cards/{id}/move")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> MoveCard(
            [FromBody] MoveCardRequest request,
            [FromRoute] Guid id)
        {
            var result = await _moveValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                return Problem(
                    result.Errors.First().ErrorMessage,
                    null,
                    StatusCodes.Status400BadRequest,
                    "Card could not be moved.");
            }

            var cardResult = await _cardService.MoveCard(new Guid(request.BoardId), new Guid(request.NewGroupId), id);

            if (cardResult.IsFailed)
            {
                return Problem(
                    cardResult.Errors.First().Message,
                    null,
                    StatusCodes.Status400BadRequest,
                    "Card could not be moved.");
            }

            return NoContent();
        }
    }
}

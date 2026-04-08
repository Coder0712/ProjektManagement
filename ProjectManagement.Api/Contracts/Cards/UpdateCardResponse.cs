using ProjectManagement.Application.Responses.Card;

namespace ProjectManagement.Api.Contracts.Cards
{
    /// <summary>
    /// Represents the response returned after updating a card, containing the updated card information.
    /// </summary>
    public sealed record UpdateCardResponse
    {
        /// <summary>
        /// Gets the updated card.
        /// </summary>
        public required CardResponse Cards { get; set; }
    }
}

using ProjectManagement.Application.Responses.Card;

namespace ProjectManagement.Api.Contracts.Cards
{
    /// <summary>
    /// Represents a response to create a card.
    /// </summary>
    public sealed record CreateCardResponse
    {
        /// <summary>
        /// Gets the card response.
        /// </summary>
        public required CardResponse Card { get; set; }
    }
}

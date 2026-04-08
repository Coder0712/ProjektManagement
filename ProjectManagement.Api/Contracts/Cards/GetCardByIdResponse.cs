namespace ProjectManagement.Api.Contracts.Cards
{
    using ProjectManagement.Application.Responses.Card;

    /// <summary>
    /// Represents a response for getting a card by id.
    /// </summary>
    public sealed record GetCardByIdResponse
    {
        /// <summary>
        /// Gets a card.
        /// </summary>
        public required CardResponse Card {  get; set; }
    }
}

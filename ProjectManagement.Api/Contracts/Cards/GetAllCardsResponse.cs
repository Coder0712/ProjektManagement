using ProjectManagement.Application.Responses.Card;

namespace ProjectManagement.Api.Contracts.Cards
{
    /// <summary>
    /// Represents the response containing the collection of all cards returned by a request.
    /// </summary>
    public sealed record GetAllCardsResponse
    {
        /// <summary>
        /// Gets or sets all cards.
        /// </summary>
        public required List<CardResponse> Cards { get; set; }
    }
}
using ProjectManagement.Domain.Cards;

namespace ProjectManagement.Domain.Tests.CardTests
{
    [TestClass]
    public class CardCreateTests
    {
        [DataRow("Test Card", "This is a test card.", 5, CardStatus.Open, 0)]
        [DataRow("Test Card", null, null, CardStatus.Open, 0)]
        [DataRow("Test Card", "", null, CardStatus.Open, 0)]
        [TestMethod]
        public void CreateCard_Succeed_WithValidValues(
            string title,
            string? description,
            int? effort,
            CardStatus cardStatus,
            int position)
        {
            // Act
            var card = Card.Create(
                title, 
                description,
                effort,
                cardStatus,
                position,
                Guid.NewGuid());

            // Assert
            Assert.IsTrue(card.IsSuccess);
            Assert.IsNotNull(card.Value);
            Assert.AreEqual(title, card.Value.Title);
            Assert.AreEqual(description, card.Value.Description);
            Assert.AreEqual(effort, card.Value.Effort);
            Assert.AreEqual(cardStatus, card.Value.Status);
            Assert.AreEqual(position, card.Value.Position);
        }

        [DataRow("", "This is a test card.", 5, CardStatus.Open, 0)]
        [DataRow("Test Card", "", -1, CardStatus.Open, 0)]
        [DataRow("Test Card", "", 1000, CardStatus.Open, 0)]
        [TestMethod]
        public async Task CreateCard_Failed_WithInvalidValues(
            string title,
            string? description,
            int? effort,
            CardStatus cardStatus,
            int position)
        {
            // Act
            var card = Card.Create(
                title, 
                description,
                effort,
                cardStatus,
                position,
                Guid.NewGuid());

            // Assert
            Assert.IsTrue(card.IsFailed);

            if (string.IsNullOrWhiteSpace(title))
            {
                Assert.AreEqual(CardErrors.TitleIsEmpty().Message, card.Errors.First().Message);
            }
            
            if(effort < 0 || effort > 999)
            {
                Assert.AreEqual(CardErrors.EffortIsInvalid().Message, card.Errors.First().Message);
            }
        }
    }
}

using ProjectManagement.Domain.Cards;

namespace ProjectManagement.Domain.Tests.CardTests
{
    [TestClass]
    public class CardUpdateTests
    {
        [TestMethod]
        public void UpdateCard_Succeed_WithValidTitle()
        {
            // Arrange
            var card = Card.Create(
                "Initial Title",
                "Initial Description",
                3,
                CardStatus.Open,
                0,
                Guid.NewGuid());

            Assert.IsNotNull(card);
            Assert.IsTrue(card.IsSuccess);

            var cardValue = card.Value;

            // Act
            var result = cardValue.UpdateTitle("Updated Title");

            var resultValue = result.Value;

            // Assert
            Assert.AreEqual("Updated Title", resultValue.Title);
            Assert.AreEqual("Initial Description", resultValue.Description);
            Assert.AreEqual(3, resultValue.Effort);
            Assert.AreEqual(CardStatus.Open, resultValue.Status);
        }

        [TestMethod]
        public void UpdateCard_Failed_WithInValidTitle()
        {
            // Arrange
            var card = Card.Create(
                "Initial Title",
                "Initial Description",
                3,
                CardStatus.Open,
                0,
                Guid.NewGuid());

            Assert.IsNotNull(card);
            Assert.IsTrue(card.IsSuccess);

            var cardValue = card.Value;

            // Act
            var result = cardValue.UpdateTitle(string.Empty);

            // Assert
            Assert.IsTrue(result.IsFailed);
            Assert.HasCount(1, result.Errors);
            Assert.AreEqual(CardErrors.TitleIsEmpty().Message, result.Errors.First().Message);
        }

        [TestMethod]
        public void UpdateCard_Succeed_WithValidDescription()
        {
            // Arrange
            var card = Card.Create(
                "Initial Title",
                "Initial Description",
                3,
                CardStatus.Open,
                0,
                Guid.NewGuid());

            Assert.IsNotNull(card);
            Assert.IsTrue(card.IsSuccess);

            var cardValue = card.Value;

            // Act
            var result = cardValue.UpdateDescription("Updated Description");
            var resultValue = result.Value;

            // Assert
            Assert.AreEqual("Initial Title", resultValue.Title);
            Assert.AreEqual("Updated Description", resultValue.Description);
            Assert.AreEqual(3, resultValue.Effort);
            Assert.AreEqual(CardStatus.Open, resultValue.Status);
        }

        [TestMethod]
        public void UpdateCard_Failed_WithInValidDescription()
        {
            // Arrange
            var card = Card.Create(
                "Initial Title",
                "Initial Description",
                3,
                CardStatus.Open,
                0,
                Guid.NewGuid());

            Assert.IsNotNull(card);
            Assert.IsTrue(card.IsSuccess);

            var cardValue = card.Value;

            // Act
            var result = cardValue.UpdateDescription(null);

            // Assert
            Assert.IsTrue(result.IsFailed);
            Assert.HasCount(1, result.Errors);
            Assert.AreEqual(CardErrors.DescriptionIsEmpty().Message, result.Errors.First().Message);
        }

        [TestMethod]
        public void UpdateCard_Succeed_WithValidEffort()
        {
            // Arrange
            var card = Card.Create(
                "Initial Title",
                "Initial Description",
                3,
                CardStatus.Open,
                0,
                Guid.NewGuid());

            Assert.IsNotNull(card);
            Assert.IsTrue(card.IsSuccess);

            var cardValue = card.Value;

            // Act
            var result = cardValue.UpdateEffort(5);
            var resultValue = result.Value;

            // Assert
            Assert.AreEqual("Initial Title", resultValue.Title);
            Assert.AreEqual("Initial Description", resultValue.Description);
            Assert.AreEqual(5, resultValue.Effort);
            Assert.AreEqual(CardStatus.Open, resultValue.Status);
        }

        [TestMethod]
        public void UpdateCard_Failed_WithInValidEffort()
        {
            // Arrange
            var card = Card.Create(
                "Initial Title",
                "Initial Description",
                3,
                CardStatus.Open,
                0,
                Guid.NewGuid());

            Assert.IsNotNull(card);
            Assert.IsTrue(card.IsSuccess);

            var cardValue = card.Value;

            // Act
            var firstResult = cardValue.UpdateEffort(-1);
            var secondResult = cardValue.UpdateEffort(1000);

            // Assert
            Assert.IsTrue(firstResult.IsFailed);
            Assert.HasCount(1, firstResult.Errors);
            Assert.AreEqual(CardErrors.EffortIsInvalid().Message, firstResult.Errors.First().Message);
            Assert.IsTrue(secondResult.IsFailed);
            Assert.HasCount(1, secondResult.Errors);
            Assert.AreEqual(CardErrors.EffortIsInvalid().Message, secondResult.Errors.First().Message);
        }

        [TestMethod]
        public void UpdateCard_Succeed_WithValidStatus()
        {
            // Arrange
            var card = Card.Create(
                "Initial Title",
                "Initial Description",
                3,
                CardStatus.Open,
                0,
                Guid.NewGuid());

            Assert.IsNotNull(card);
            Assert.IsTrue(card.IsSuccess);

            var cardValue = card.Value;

            // Act
            var result = cardValue.UpdateStatus(CardStatus.InProgress);

            var resultValue = result.Value;

            // Assert
            Assert.AreEqual("Initial Title", resultValue.Title);
            Assert.AreEqual("Initial Description", resultValue.Description);
            Assert.AreEqual(3, resultValue.Effort);
            Assert.AreEqual(CardStatus.InProgress, resultValue.Status);
        }

        [TestMethod]
        public void MovedCard_Succeed_WithValidValues()
        {
            // Arrange
            var card = Card.Create(
                "Initial Title",
                "Initial Description",
                3,
                CardStatus.Open,
                0,
                Guid.NewGuid());

            Assert.IsNotNull(card);
            Assert.IsTrue(card.IsSuccess);

            var cardValue = card.Value;

            // Act
            var result = cardValue.MoveCard(Guid.NewGuid(), 1);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void MovedCard_Failed_WithInValidPosition()
        {
            // Arrange
            var card = Card.Create(
                "Initial Title",
                "Initial Description",
                3,
                CardStatus.Open,
                0,
                Guid.NewGuid());

            Assert.IsNotNull(card);
            Assert.IsTrue(card.IsSuccess);

            var cardValue = card.Value;
            // Act

            var result = cardValue.MoveCard(Guid.NewGuid(), -1);

            // Assert
            Assert.IsTrue(result.IsFailed);
            Assert.HasCount(1, result.Errors);
            Assert.AreEqual(CardErrors.PositionIsNegative().Message, result.Errors.First().Message);
        }

        [TestMethod]
        public void MovedCard_Failed_WithInValidGroupId()
        {
            // Arrange
            var card = Card.Create(
                "Initial Title",
                "Initial Description",
                3,
                CardStatus.Open,
                0,
                Guid.NewGuid());

            Assert.IsNotNull(card);
            Assert.IsTrue(card.IsSuccess);

            var cardValue = card.Value;

            // Act
            var result = cardValue.MoveCard(Guid.Empty, 1);

            // Assert
            Assert.IsTrue(result.IsFailed);
            Assert.HasCount(1, result.Errors);
            Assert.AreEqual(CardErrors.GroupIdIsEmpty().Message, result.Errors.First().Message);
        }
    }
}

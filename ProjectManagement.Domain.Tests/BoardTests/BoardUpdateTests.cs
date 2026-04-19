using FakeItEasy;
using ProjectManagement.Domain.Boards;
using ProjectManagement.Domain.Boards.Errors;
using ProjectManagement.Domain.Services;

namespace ProjectManagement.Domain.Tests.BoardTests
{
    [TestClass]
    public class BoardUpdateTests
    {
        IBoardProjectUniquessChecker checker;

        [TestInitialize]
        public void ProjectTestInitialize()
        {
            checker = A.Fake<IBoardProjectUniquessChecker>();
        }

        [TestMethod]
        public async Task UpdateBoard_Succeed_WithValidValues()
        {
            // Arrange
            var board = await Board.Create(
                checker,
                "Test",
                Guid.NewGuid());

            Assert.IsTrue(board.IsSuccess);
            Assert.IsNotNull(board.Value);

            var boardValue = board.Value;

            // Act
            var updatedBoard = boardValue.UpdateTitle("New Title");

            // Assert
            Assert.IsTrue(updatedBoard.IsSuccess);
            Assert.AreEqual("New Title", boardValue.Title);
        }

        [TestMethod]
        public async Task UpdateBoard_Failed_WithEmptyTitle()
        {
            // Arrange
            var board = await Board.Create(
                checker,
                "Test",
                Guid.NewGuid());

            Assert.IsTrue(board.IsSuccess);
            Assert.IsNotNull(board.Value);

            var boardValue = board.Value;

            // Act
            var result = boardValue.UpdateTitle("");

            // Assert
            Assert.IsTrue(result.IsFailed);
            Assert.AreEqual(BoardErrors.TitleIsEmpty().Message, result.Errors[0].Message);
        }
    }
}

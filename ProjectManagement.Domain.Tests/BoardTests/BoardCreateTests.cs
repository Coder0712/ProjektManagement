using FakeItEasy;
using ProjectManagement.Domain.Boards;
using ProjectManagement.Domain.Boards.Errors;
using ProjectManagement.Domain.Services;

namespace ProjectManagement.Domain.Tests.BoardTests
{
    [TestClass]
    public class BoardCreateTests
    {
        IBoardProjectUniquessChecker checker;

        [TestInitialize]
        public void ProjectTestInitialize()
        {
            checker = A.Fake<IBoardProjectUniquessChecker>();
        }

        [TestMethod]
        public async Task CreateBoard_Succeed_WithValidValues()
        {
            // Act
            var board = await Board.Create(
                checker,
                "Test",
                Guid.NewGuid());

            // Assert
            Assert.IsTrue(board.IsSuccess);
            Assert.AreEqual("Test", board.Value.Title);
        }

        [TestMethod]
        public async Task CreateBoard_Failed_WithEmptyTitle()
        {
            // Act
            var board = await Board.Create(
                checker,
                "",
                Guid.NewGuid());

            // Assert
            Assert.IsTrue(board.IsFailed);
            Assert.AreEqual(BoardErrors.TitleIsEmpty().Message, board.Errors[0].Message);
        }

        [TestMethod]
        public async Task CreateBoard_Failed_WithEmptyProjectId()
        {
            // Act
            var board = await Board.Create(
                checker,
                "Test",
                Guid.Empty);

            // Assert
            Assert.IsTrue(board.IsFailed);
            Assert.AreEqual(BoardErrors.ProjectIdIsEmpty().Message, board.Errors[0].Message);
        }
    }
}

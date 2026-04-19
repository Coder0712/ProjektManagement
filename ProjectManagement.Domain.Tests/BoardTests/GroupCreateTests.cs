using FakeItEasy;
using ProjectManagement.Domain.Boards;
using ProjectManagement.Domain.Services;

namespace ProjectManagement.Domain.Tests.BoardTests
{
    [TestClass]
    public class GroupCreateTests
    {
        IBoardProjectUniquessChecker checker;

        [TestInitialize]
        public void GroupTestInitialize()
        {
            checker = A.Fake<IBoardProjectUniquessChecker>();
        }

        [TestMethod]
        public async Task CreateGroup_Succeed_WithValidValues()
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
            var group = boardValue.CreateGroup("Test");

            // Assert
            Assert.IsTrue(group.IsSuccess);
            Assert.IsNotNull(group.Value);
            Assert.AreEqual("Test", group.Value.Title);
        }

        [TestMethod]
        public async Task CreateGroup_Failed_WithEmptyTitle()
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
            var group = boardValue.CreateGroup("");

            // Assert
            Assert.IsTrue(group.IsFailed);
            Assert.AreEqual("Group title is empty.", group.Errors[0].Message);
        }

        [TestMethod]
        public async Task CreateGroup_Failed_SameGroupTitle()
        {
            // Arrange
            var board = await Board.Create(
                checker,
                "Test",
                Guid.NewGuid());

            Assert.IsTrue(board.IsSuccess);
            Assert.IsNotNull(board.Value);

            var boardValue = board.Value;

            var group = boardValue.CreateGroup("Test");
            Assert.IsTrue(group.IsSuccess);
            Assert.IsNotNull(group.Value);

            // Act
            var group2 = boardValue.CreateGroup("Test");

            // Assert
            Assert.IsTrue(group2.IsFailed);
            Assert.AreEqual("A Group with this title already exists.", group2.Errors[0].Message);
        }
    }
}

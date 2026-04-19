using FakeItEasy;
using ProjectManagement.Domain.Boards;
using ProjectManagement.Domain.Boards.Errors;
using ProjectManagement.Domain.Services;

namespace ProjectManagement.Domain.Tests.BoardTests
{
    [TestClass]
    public class GroupUpdateTests
    {
        IBoardProjectUniquessChecker checker;

        [TestInitialize]
        public void ProjectTestInitialize()
        {
            checker = A.Fake<IBoardProjectUniquessChecker>();
        }

        [TestMethod]
        public async Task UpdateGroup_Succeed_WithValidValues()
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
            var group = boardValue.CreateGroup("Test Group");

            Assert.IsTrue(group.IsSuccess);
            Assert.IsNotNull(group.Value);

            var groupValue = group.Value;

            var updatedGroup = boardValue.UpdateGroup(groupValue.Title, "New Group Title");

            // Assert
            Assert.IsTrue(updatedGroup.IsSuccess);
            Assert.AreEqual("New Group Title", groupValue.Title);
        }

        [TestMethod]
        public async Task UpdateGroup_Failed_WhenGroupNotFound()
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
            var group = boardValue.CreateGroup("Test Group");

            Assert.IsTrue(group.IsSuccess);
            Assert.IsNotNull(group.Value);

            var groupValue = group.Value;

            var result = boardValue.UpdateGroup("", "New group title.");

            // Assert
            Assert.IsTrue(result.IsFailed);
            Assert.AreEqual(GroupErrors.GroupNotFound().Message, result.Errors[0].Message);
        }

        [TestMethod]
        public async Task UpdateGroup_Succeed_WithSameTitle()
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
            var group = boardValue.CreateGroup("Test Group");

            Assert.IsTrue(group.IsSuccess);
            Assert.IsNotNull(group.Value);

            var groupValue = group.Value;

            var result = boardValue.UpdateGroup(groupValue.Title, "Test Group");

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
        }

        [TestMethod]
        public async Task UpdateGroup_Failed_WhenGroupTitleIsTheSame()
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
            var group = boardValue.CreateGroup("Test Group");

            Assert.IsTrue(group.IsSuccess);
            Assert.IsNotNull(group.Value);

            var groupValue = group.Value;

            var secondGroup = boardValue.CreateGroup("Second Test Group");

            Assert.IsTrue(group.IsSuccess);
            Assert.IsNotNull(group.Value);

            var secondGroupValue = secondGroup.Value;

            var result = boardValue.UpdateGroup(secondGroupValue.Title, "Test Group");

            // Assert
            Assert.IsTrue(result.IsFailed);
            Assert.AreEqual(GroupErrors.GroupTitleAlreadyExists().Message, result.Errors[0].Message);
        }
    }
}

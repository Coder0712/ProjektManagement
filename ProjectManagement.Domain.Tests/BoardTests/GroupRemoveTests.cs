using FakeItEasy;
using ProjectManagement.Domain.Boards;
using ProjectManagement.Domain.Boards.Errors;
using ProjectManagement.Domain.Services;

namespace ProjectManagement.Domain.Tests.BoardTests
{
    [TestClass]
    public class GroupRemoveTests
    {
        IBoardProjectUniquessChecker checker;

        [TestInitialize]
        public void ProjectTestInitialize()
        {
            checker = A.Fake<IBoardProjectUniquessChecker>();
        }

        [TestMethod]
        public async Task RemoveGroup_Succeed_WhenGroupExists()
        {
            // Arrange
            var board = await Board.Create(
                checker,
                "Test",
                Guid.NewGuid());

            Assert.IsTrue(board.IsSuccess);
            Assert.IsNotNull(board.Value);

            var boardValue = board.Value;

            var group = boardValue.CreateGroup("Test Group");

            Assert.IsTrue(group.IsSuccess);
            Assert.IsNotNull(group.Value);

            Assert.HasCount(1, boardValue.Groups);

            var groupValue = group.Value;

            // Act
            var result = boardValue.RemoveGroup(groupValue);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.HasCount(0, boardValue.Groups);
        }

        [TestMethod]
        public async Task RemoveGroup_Failed_WhenGroupNotFound()
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
            var result = boardValue.RemoveGroup(null);

            // Assert
            Assert.IsTrue(result.IsFailed);
            Assert.AreEqual(GroupErrors.GroupNotFound().Message, result.Errors.FirstOrDefault().Message);
            Assert.HasCount(0, boardValue.Groups);
        }
    }
}

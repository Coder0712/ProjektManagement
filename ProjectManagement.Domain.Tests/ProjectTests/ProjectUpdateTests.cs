using FakeItEasy;
using ProjectManagement.Domain.Projects;
using ProjectManagement.Domain.Projects.Errors;
using ProjectManagement.Domain.Services;

namespace ProjectManagement.Domain.Tests.ProjectTests
{
    [TestClass]
    public class ProjectUpdateTests
    {
        IProjectTitleUniquenessChecker projectTitleUniquenessChecker;

        [TestInitialize]
        public void ProjectTestInitialize()
        {
            projectTitleUniquenessChecker = A.Fake<IProjectTitleUniquenessChecker>();
        }

        [TestMethod]
        public async Task UpdateName_Succeed_WithValidValue()
        {
            // Arrange
            var project =
                await Project.Create(
                    projectTitleUniquenessChecker,
                    "Test",
                    "Test",
                    ProjectStatus.Open);

            Assert.IsTrue(project.IsSuccess);
            Assert.IsNotNull(project.Value);

            var projectValue = project.Value;

            // Act
            projectValue.UpdateName("New Test");

            // Assert
            Assert.IsTrue(project.IsSuccess);
            Assert.AreEqual("New Test", projectValue.Name);
        }

        [TestMethod]
        public async Task UpdateName_Failed_WithInvalidValues()
        {
            // Arrange
            var project =
                await Project.Create(
                    projectTitleUniquenessChecker,
                    "Test",
                    "Test",
                    ProjectStatus.Open);

            Assert.IsTrue(project.IsSuccess);
            Assert.IsNotNull(project.Value);

            var projectValue = project.Value;

            // Act
            var result = projectValue.UpdateName("");

            // Assert
            Assert.IsTrue(result.IsFailed);
            Assert.AreEqual(ProjectErrors.TitleIsEmpty().Message, result.Errors[0].Message);
        }

        [TestMethod]
        public async Task UpdateDescription_Succeed_WithValidValue()
        {
            // Arrange
            var project =
                await Project.Create(
                    projectTitleUniquenessChecker,
                    "Test",
                    "Test",
                    ProjectStatus.Open);

            Assert.IsTrue(project.IsSuccess);
            Assert.IsNotNull(project.Value);

            var projectValue = project.Value;

            // Act
            projectValue.UpdateDescription("New Test");

            // Assert
            Assert.IsTrue(project.IsSuccess);
            Assert.AreEqual("New Test", projectValue.Description);
        }

        [TestMethod]
        public async Task UpdateDescription_Failed_WithInvalidValue()
        {
            // Arrange
            var project =
                await Project.Create(
                    projectTitleUniquenessChecker,
                    "Test",
                    "Test",
                    ProjectStatus.Open);

            Assert.IsTrue(project.IsSuccess);
            Assert.IsNotNull(project.Value);

            var projectValue = project.Value;

            // Act
            var result = projectValue.UpdateDescription("");

            // Assert
            Assert.IsTrue(result.IsFailed);
            Assert.AreEqual(ProjectErrors.DescriptionIsEmpty().Message, result.Errors[0].Message);
        }

        [TestMethod]
        public async Task UpdateStatus_Succeed_WithValidValue()
        {
            // Arrange
            var project =
                await Project.Create(
                    projectTitleUniquenessChecker,
                    "Test",
                    "Test",
                    ProjectStatus.Open);

            Assert.IsTrue(project.IsSuccess);
            Assert.IsNotNull(project.Value);

            var projectValue = project.Value;

            // Act
            projectValue.UpdateStatus(ProjectStatus.InProgress);

            // Assert
            Assert.IsTrue(project.IsSuccess);
            Assert.AreEqual(ProjectStatus.InProgress, projectValue.Status);
        }
    }
}
using FakeItEasy;
using ProjectManagement.Domain.Projects;
using ProjectManagement.Domain.Projects.Errors;
using ProjectManagement.Domain.Services;

namespace ProjectManagement.Domain.Tests.ProjectTests
{
    [TestClass]
    public class ProjectCreateTests
    {
        IProjectTitleUniquenessChecker projectTitleUniquenessChecker;

        [TestInitialize]
        public void ProjectTestInitialize()
        {
            projectTitleUniquenessChecker = A.Fake<IProjectTitleUniquenessChecker>();
        }

        [TestMethod]
        public async Task CreateProject_Succeed_WithValidValues()
        {
            var project =
                await Project.Create(
                    projectTitleUniquenessChecker,
                    "Test",
                    "Test",
                    ProjectStatus.Open);

            Assert.IsTrue(project.IsSuccess);

            Assert.AreEqual("Test", project.Value.Name);
            Assert.AreEqual("Test", project.Value.Description);
            Assert.AreEqual(ProjectStatus.Open, project.Value.Status);
        }

        [DataRow("", "Test")]
        [DataRow("Test", "")]
        [DataRow("", "")]
        [TestMethod]
        public async Task CreateProject_Failed_WithEmptyStrings(string name, string description)
        {
            var project =
                await Project.Create(
                    projectTitleUniquenessChecker,
                    name,
                    description,
                    ProjectStatus.Open);

            Assert.IsTrue(project.IsFailed);
            Assert.IsNotEmpty(project.Errors);

            if (string.IsNullOrWhiteSpace(name))
            {
                Assert.AreEqual(ProjectErrors.TitleIsEmpty().Message, project.Errors[0].Message);
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    Assert.AreEqual(ProjectErrors.TitleIsEmpty().Message, project.Errors[0].Message);
                    return;
                }

                Assert.AreEqual(ProjectErrors.DescriptionIsEmpty().Message, project.Errors[0].Message);
            }
        }

        [TestMethod]
        public async Task CreateProject_Failed_WithNonUniqueTitle()
        {
            A.CallTo(() => projectTitleUniquenessChecker.ProjectAlreadyHasTitle(A<string>.Ignored))
                .Returns(true);

            var project =
                await Project.Create(
                    projectTitleUniquenessChecker,
                    "Test",
                    "Test",
                    ProjectStatus.Open);

            Assert.IsTrue(project.IsFailed);
            Assert.IsNotEmpty(project.Errors);
            Assert.AreEqual(ProjectErrors.ProjectHasTitle().Message, project.Errors[0].Message);
        }
    }
}

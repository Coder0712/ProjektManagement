using ProjectManagement.Domain.Projects;
using ProjectManagement.Domain.Services;

namespace ProjectManagement.Infrastructure.Services
{
    public sealed class ProjectTitleUniquenessChecker
        : IProjectTitleUniquenessChecker
    {
        private readonly IProjectRepository _repository;

        public ProjectTitleUniquenessChecker(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ProjectAlreadyHasTitle(string title)
            => await _repository.CheckExistenceAsync(title);
    }
}

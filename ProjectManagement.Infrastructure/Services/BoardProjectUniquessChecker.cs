using ProjectManagement.Domain.Boards;
using ProjectManagement.Domain.Services;

namespace ProjectManagement.Infrastructure.Services
{
    public class BoardProjectUniquessChecker
        : IBoardProjectUniquessChecker
    {
        private readonly IBoardRepository _repository;

        public BoardProjectUniquessChecker(
            IBoardRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ProjectAlreadyAssignedAsync(Guid projectId)
            => await _repository.SearchBoardWithProjectAsync(projectId);
    }
}

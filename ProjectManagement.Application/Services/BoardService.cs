using FluentResults;
using Microsoft.Extensions.Logging;
using ProjectManagement.Application.Errors;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Application.Mapping;
using ProjectManagement.Application.Responses.Board;
using ProjectManagement.Domain.Boards;
using ProjectManagement.Domain.Cards;
using ProjectManagement.Domain.Common;
using ProjectManagement.Domain.Services;

namespace ProjectManagement.Application.Services
{
    /// <summary>
    /// Represents the board service.
    /// </summary>
    public sealed class BoardService : IBoardService
    {
        private readonly IBoardRepository _repository;
        private readonly ICardRepository _cardRepository;
        private readonly IBoardProjectUniquessChecker _boardProjectUniquessChecker;
        private readonly ILogger<BoardService> _logger;
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initialize a new object of type <see cref="BoardService"/>.
        /// </summary>
        /// <param name="repository">The board repository.</param>
        /// <param name="dbContext">The db context.</param>
        public BoardService(
            ICardRepository cardRepository,
            IBoardRepository repository,
            IBoardProjectUniquessChecker boardProjectUniquessChecker,
            ILogger<BoardService> logger,
            IDbContext dbContext)
        {
            _cardRepository = cardRepository;
            _repository = repository;
            _boardProjectUniquessChecker = boardProjectUniquessChecker;
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <inheritdoc/> 
        public async Task<Result<BoardResponse>> CreateBoardAsync(
            string name,
            Guid projectId,
            CancellationToken cancellationToken = default)
        {
            var boardResult = await Board.Create(_boardProjectUniquessChecker, name, projectId);

            if (boardResult.IsFailed)
            {
                _logger.LogWarning("Failed to create board with name {Name} for project with id {ProjectId}. Errors: {Errors}",
                    name,
                    projectId,
                    boardResult.Errors);

                return Result.Fail(boardResult.Errors);
            }

            await _repository.AddAsync(boardResult.Value);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Board with name {Name} for project with id {ProjectId} created successfully with id {BoardId}.",
                name,
                projectId,
                boardResult.Value.Id);

            return Result.Ok(BoardMapper.MapBoardToBoardResponse(boardResult.Value));
        }

        /// <inheritdoc/> 
        public async Task<Result<BoardResponse>> UpdateBoardAsync(
            Guid id,
            string? name,
            CancellationToken cancellationToken = default)
        {
            var board = await _repository.GetBoardByIdAsync(id);

            if (board is null)
            {
                _logger.LogWarning("Failed to update board with id {BoardId}. Board not found.", id);

                return Result.Fail(ApplicationErrors.Board.NotFound(id));
            }

            if(name is not null)
            {
                var boardResult = board.UpdateTitle(name);

                if (boardResult.IsFailed)
                {
                    _logger.LogWarning("Failed to update board with id {BoardId}. Errors: {Errors}",
                        id,
                        boardResult.Errors);

                    return Result.Fail(boardResult.Errors);
                }

                _repository.Update(board);

                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Board with id {BoardId} updated successfully.", id);
            }

            return Result.Ok(BoardMapper.MapBoardToBoardResponse(board));
        }

        /// <inheritdoc/> 
        public async Task<Result<BoardPagingResponse>> GetAllBoardsAsync(int page, int items)
        {
            var boards = await _repository.GetAllBoardsAsync(page, items);

            var totalCount = await _repository.CountAsync();

            var boardsList = boards.Select(BoardMapper.MapToBoardResponse).ToList();

            _logger.LogInformation("Retrieved boards for page {Page} with {Items} items per page.", page, items);

            return Result.Ok(new BoardPagingResponse
            {
                Page = page,
                Items = items,
                TotalCount = totalCount,
                Boards = boardsList
            });
        }

        /// <inheritdoc/> 
        public async Task<Result> DeleteBoardAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var board = await _repository.GetBoardByIdAsync(id);

            if (board is null)
            {
                _logger.LogWarning("Failed to delete board with id {BoardId}. Board not found.", id);

                return Result.Fail(ApplicationErrors.Board.NotFound(id));
            }

            _repository.Remove(board);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Board with id {BoardId} deleted successfully.", id);

            return Result.Ok();
        }

        /// <inheritdoc/> 
        public async Task<Result<BoardWithGroupsAndCardsResponse>> GetBoardByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);

            if(result is null)
            {
                _logger.LogWarning("Failed to retrieve board with id {BoardId}. Board not found.", id);

                return Result.Fail(ApplicationErrors.Board.NotFound(id));
            }

            var groups = result.Groups;

            var cards = await _cardRepository.GetAllAsync();

            var cardsForBoard = cards.Where(c => groups.All(g => g.Id == c.GroupId)).ToList();

            _logger.LogInformation("Board with id {BoardId} retrieved successfully.", id);

            return Result.Ok(BoardMapper.MapToResponse(result, cards));
        }

        /// <inheritdoc/> 
        public async Task<Result<BoardWithGroupsResponse>> CreateGroupAsync(
            string title,
            Guid boardId,
            CancellationToken cancellationToken = default)
        {
            var board = await _repository.GetBoardByIdAsync(boardId);

            if(board is null)
            {
                _logger.LogWarning("Failed to create group with title {Title} for board with id {BoardId}. Board not found.",
                    title,
                    boardId);

                return Result.Fail<BoardWithGroupsResponse>(ApplicationErrors.Board.NotFound(boardId));
            }

            var groupResult = board.CreateGroup(title);

            if (groupResult.IsFailed)
            {
                _logger.LogWarning("Failed to create group with title {Title} for board with id {BoardId}. Errors: {Errors}",
                    title,
                    boardId,
                    groupResult.Errors);

                return Result.Fail(groupResult.Errors);
            }

            await _repository.AddGroup(groupResult.Value);

            _repository.Update(board);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Group with title {Title} created successfully for board with id {BoardId}.",
                title,
                boardId);

            return BoardMapper.MapToBoardWithGroupResponse(board);
        }

        /// <inheritdoc/> 
        public async Task<Result> DeleteGroupAsync(Guid boardId, Guid groupId, CancellationToken cancellationToken = default)
        {
            var board = await _repository.GetBoardByIdAsync(boardId);

            if(board is null)
            {
                _logger.LogWarning("Failed to delete group with id {GroupId} for board with id {BoardId}. Board not found.",
                    groupId,
                    boardId);

                return Result.Fail(ApplicationErrors.Board.NotFound(boardId));
            }

            var group = board.Groups.FirstOrDefault(g => g.Id == groupId);

            if (group is null)
            {
                _logger.LogWarning("Failed to delete group with id {GroupId} for board with id {BoardId}. Group not found.",
                    groupId,
                    boardId);

                return Result.Fail(ApplicationErrors.Board.GroupNotFound(groupId));
            }

            var removeResult = board.RemoveGroup(group);

            if (removeResult.IsFailed)
            {
                return Result.Fail(removeResult.Errors);
            }

            _repository.Update(board);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }

        /// <inheritdoc/> 
        public async Task<Result<BoardWithGroupsResponse>> UpdateGroupAsync(
            Guid boardId,
            Guid groupId,
            string oldTitle,
            string? title,
            CancellationToken cancellationToken = default)
        {
            var board = await _repository.GetBoardByIdAsync(boardId);

            if(board is null)
            {
                _logger.LogWarning("Failed to update group with id {GroupId} for board with id {BoardId}. Board not found.",
                    groupId,
                    boardId);

                return Result.Fail(ApplicationErrors.Board.NotFound(boardId));
            }

            var group = board.Groups.FirstOrDefault(g => g.Id == groupId);

            if (group is null)
            {
                _logger.LogWarning("Failed to update group with id {GroupId} for board with id {BoardId}. Group not found.",
                    groupId,
                    boardId);

                return Result.Fail(ApplicationErrors.Board.GroupNotFound(groupId));
            }

            if(title is not null)
            {
                var updateResult = board.UpdateGroup(oldTitle, title);

                if (updateResult.IsFailed)
                {
                    _logger.LogWarning("Failed to update group with id {GroupId} for board with id {BoardId}. Errors: {Errors}",
                        groupId,
                        boardId,
                        updateResult.Errors);

                    return Result.Fail(updateResult.Errors);
                }

                _repository.Update(board);

                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Group with id {GroupId} updated successfully for board with id {BoardId}.",
                    groupId,
                    boardId);
            }

            return Result.Ok(BoardMapper.MapToBoardWithGroupResponse(board));
        }
    }
}

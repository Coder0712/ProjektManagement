using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Contracts.Boards;
using ProjectManagement.Api.Contracts.Groups;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Domain.Common.Errors;

namespace ProjectManagement.Controllers
{
    /// <summary>
    /// Controller for the board endpoints.
    /// </summary>
    [Route("")]
    [Authorize(Roles = "user")]
    [ApiController]
    public sealed class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;
        private readonly IValidator<CreateBoardRequest> _validator;
        private readonly IValidator<UpdateBoardRequst> _updateValidator;

        public BoardController(
            IBoardService boardService,
            IValidator<CreateBoardRequest> validator,
            IValidator<UpdateBoardRequst> updateValidator)
        {
            _boardService = boardService;
            _validator = validator;
            _updateValidator = updateValidator;
        }

        /// <summary>
        /// Creates a board.
        /// </summary>
        /// <param name="request"><see cref="CreateBoardRequest"/>.</param>
        /// <returns>The created board.</returns>
        [Route("/api/project-management/boards")]
        [HttpPost]
        [ProducesResponseType(typeof(CreateBoardResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBoard([FromBody] CreateBoardRequest request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                return Problem(
                    result.Errors.First().ErrorMessage,
                    null,
                    StatusCodes.Status400BadRequest,
                    "Board can not be created.");
            }

            var boardResult = await _boardService.CreateBoardAsync(request.Title, new Guid(request.ProjectId));

            if (boardResult.IsFailed)
            {
                return Problem(
                    boardResult.Errors.First().Message,
                    null,
                    StatusCodes.Status400BadRequest,
                    "Board can not be created.");
            }

            return CreatedAtAction("GetById", new { id = boardResult.Value.Id }, boardResult.Value);
        }

        /// <summary>
        /// Get all boards.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="items">Number of items per page.</param>
        /// <returns></returns>
        [Route("/api/project-management/boards")]
        [HttpGet]
        [ProducesResponseType(typeof(GetAllBoardsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBoards(
            [FromQuery] int page = 1,
            [FromQuery] int items = 10)
        {
            var boardResult = await _boardService.GetAllBoardsAsync(page, items);

            return Ok(boardResult.Value);
        }

        /// <summary>
        /// Gets a board by the id.
        /// </summary>
        /// <param name="id">The id of the board.</param>
        /// <returns>A single board.</returns>
        [Route("/api/project-management/boards/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(GetBoardByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id)
        {
            var boardResult = await _boardService.GetBoardByIdAsync(id);

            if (boardResult.IsFailed)
            {
                return Problem(
                        boardResult.Errors.First().Message,
                        null,
                        StatusCodes.Status404NotFound,
                        "Board can not be found.");
            }

            return Ok(boardResult.Value);
        }

        /// <summary>
        /// Updates a board.
        /// </summary>
        /// <param name="id">The id of the board.</param>
        /// <param name="request"><see cref="UpdateBoardRequst"/>.</param>
        /// <returns>The updated board.</returns>
        [Route("/api/project-management/boards/{id}")]
        [HttpPatch]
        [ProducesResponseType(typeof(UpdateBoardResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBoard(
            [FromRoute] Guid id,
            [FromBody] UpdateBoardRequst request)
        {
            var result = await _updateValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                return Problem(
                    result.Errors.First().ErrorMessage,
                    null,
                    StatusCodes.Status400BadRequest,
                    "Board can not be updated.");
            }

            var boardResult = await _boardService.UpdateBoardAsync(id, request.Title);

            if (boardResult.IsFailed)
            {
                if (boardResult.Errors.OfType<NotFoundError>().Any())
                {
                    return Problem(
                        boardResult.Errors.First().Message,
                        null,
                        StatusCodes.Status404NotFound,
                        "Board can not be updated.");
                }
                else
                {
                    return Problem(
                        boardResult.Errors.First().Message,
                        null,
                        StatusCodes.Status400BadRequest,
                        "Board can not be updated.");
                }
            }

            return Ok(boardResult.Value);
        }

        /// <summary>
        /// Deletes a board.
        /// </summary>
        /// <param name="id">The id of the board.</param>
        /// <returns>No content.</returns>
        [Route("/api/project-management/boards/{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBoardById(
            [FromRoute] Guid id)
        {
            var boardResult = await _boardService.DeleteBoardAsync(id);

            if (boardResult.IsFailed)
            {
                return Problem(
                        boardResult.Errors.First().Message,
                        null,
                        StatusCodes.Status404NotFound,
                        "Board can not be deleted.");
            }

            return NoContent();
        }

        /// <summary>
        /// Adds a group to the board.
        /// </summary>
        /// <param name="id">The id of the board.</param>
        /// <param name="request"><see cref="CreateGroupRequest"/>.</param>
        /// <returns></returns>
        [Route("/api/project-management/boards/{id}/groups")]
        [HttpPost]
        [ProducesResponseType(typeof(GetBoardByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateGroup(
            [FromRoute] Guid id,
            [FromBody] CreateGroupRequest request)
        {
            var boardResult = await _boardService.CreateGroupAsync(request.Title, id);

            if (boardResult.IsFailed)
            {
                if (boardResult.Errors.OfType<NotFoundError>().Any())
                {
                    return Problem(
                        boardResult.Errors.First().Message,
                        null,
                        StatusCodes.Status404NotFound,
                        "Group could not be added.");
                }
                else
                {
                    return Problem(
                        boardResult.Errors.First().Message,
                        null,
                        StatusCodes.Status400BadRequest,
                        "Group could not be added.");
                }
            }

            return Ok(boardResult.Value);
        }

        /// <summary>
        /// Updates a group.
        /// </summary>
        /// <param name="boardId">The board id.</param>
        /// <param name="groupId">The group id.</param>
        /// <param name="request"><see cref="UpdateGroupRequest"/>.</param>
        /// <returns>No content.</returns>
        [Route("/api/project-management/boards/{boardId}/groups/{groupId}")]
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateGroup(
            [FromRoute] Guid boardId,
            [FromRoute] Guid groupId,
            [FromBody] UpdateGroupRequest request)
        {
            var boardResult = await _boardService.UpdateGroupAsync(boardId, groupId, request.CurrentTitle, request.NewTitle);

            if (boardResult.IsFailed)
            {
                return Problem(
                        boardResult.Errors.First().Message,
                        null,
                        StatusCodes.Status400BadRequest,
                        "Group could not be updated.");
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes a group from the specified board.
        /// </summary>
        /// <param name="boardId">The board id.</param>
        /// <param name="groupId">The group id.</param>
        /// <returns>No content.</returns>
        [Route("/api/project-management/boards/{boardId}/groups/{groupId}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGroup(
            [FromRoute] Guid boardId,
            [FromRoute] Guid groupId)
        {
            var boardResult = await _boardService.DeleteGroupAsync(boardId, groupId);

            if (boardResult.IsFailed)
            {
                return Problem(
                        boardResult.Errors.First().Message,
                        null,
                        StatusCodes.Status404NotFound,
                        "Group could not be deleted.");
            }

            return NoContent();
        }
    }
}

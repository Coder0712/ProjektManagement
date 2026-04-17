using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Contracts.Projects;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Contracts.Projects;
using ProjectManagement.Domain.Common.Errors;
using ProjectManagement.Domain.Projects;

namespace ProjectManagement.Controllers
{
    /// <summary>
    /// Controller for the project endpoints.
    /// </summary>
    [Route("")]
    [Authorize(Roles = "user")]
    [ApiController]
    public sealed class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IValidator<CreateProjectRequest> _createValidator;
        private readonly IValidator<UpdateProjectRequest> _updateValidator;

        public ProjectController(
            IProjectService projectService,
            IValidator<CreateProjectRequest> createValidator,
            IValidator<UpdateProjectRequest> updateValidator)
        {
            _projectService = projectService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="request"><see cref="CreateProjectRequest"/>.</param>
        /// <returns>Á new project.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [Route("/api/project-management/projects")]
        [HttpPost]
        [ProducesResponseType(typeof(CreateProjectResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
        {
            var result = await this._createValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                return Problem(
                    result.Errors.First().ErrorMessage,
                    null,
                    StatusCodes.Status400BadRequest,
                    "Project can not be created.");
            }

            var projectStatus = Enum.Parse<ProjectStatus>(request.Status, true);

            var projectResult = await this._projectService.CreateProjectAsync(
                request.Name,
                request.Description,
                projectStatus);

            if (projectResult.IsFailed)
            {
                return Problem(
                    projectResult.Errors.First().Message,
                    null,
                    StatusCodes.Status400BadRequest,
                    "Project can not be created.");
            }

            return CreatedAtAction("GetById", new { id = projectResult.Value.Id }, projectResult.Value);

        }

        /// <summary>
        /// Gets all projects.
        /// </summary>
        /// <returns>A list of all projects.</returns>
        [Route("/api/project-management/projects")]
        [HttpGet]
        [ProducesResponseType(typeof(GetAllProjectsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int items = 10)
        {
            if (page <= 0)
            {
                page = 1;
            }

            if (items < 0)
            {
                items = 10;
            }

            var projectResult = await this._projectService.GetAllProjectsAsync(page, items);

            var projectPagingResponse = projectResult.Value;

            return Ok(
                new GetAllProjectsResponse
                {
                    Page = projectResult.Value.Page,
                    Items = projectPagingResponse.Items,
                    TotalCount = projectPagingResponse.TotalCount,
                    Projects = projectResult.Value.Projects,
                });
        }

        /// <summary>
        /// Gets a project by the id.
        /// </summary>
        /// <param name="id">The id of the project.</param>
        /// <returns>A single project.</returns>
        [Route("/api/project-management/projects/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(GetProjectByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var projectResult = await this._projectService.GetProjectByIdAsync(id);

            if (projectResult.IsFailed)
            {
                return Problem(
                        projectResult.Errors.First().Message,
                        null,
                        StatusCodes.Status404NotFound,
                        "Project could not be found.");
            }

            return Ok(new GetProjectByIdResponse
            {
                Project = projectResult.Value
            });
        }

        /// <summary>
        /// Updates a new project.
        /// </summary>
        /// <param name="id">The id of the project.</param>
        /// <param name="request"><see cref="UpdateProjectRequest"/>.</param>
        /// <returns>The updated project.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [Route("/api/project-management/projects/{id}")]
        [HttpPatch]
        [ProducesResponseType(typeof(UpdateProjectResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] UpdateProjectRequest request)
        {
            var result = await this._updateValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                return Problem(
                    result.Errors.First().ErrorMessage,
                    null,
                    StatusCodes.Status404NotFound,
                    "Project could not be updated.");
            }

            var projectStatus = Enum.Parse<ProjectStatus>(request.Status, true);

            var projectResult = await this._projectService.UpdateProjectAsync(id, request.Name, request.Description, projectStatus);

            if (projectResult.IsFailed)
            {
                if (projectResult.Errors.OfType<NotFoundError>().Any())
                {
                    return Problem(
                        projectResult.Errors.First().Message,
                        null,
                        StatusCodes.Status404NotFound,
                        "Project could not be updated.");
                }
                else
                {
                    return Problem(
                       projectResult.Errors.First().Message,
                       null,
                       StatusCodes.Status400BadRequest,
                       "Project could not be updated.");
                }
            }

            return Ok(new UpdateProjectResponse
            {
                Project = projectResult.Value
            });
        }

        /// <summary>
        /// Deletes a project by the id.
        /// </summary>
        /// <param name="id">The id of the project.</param>
        /// <returns></returns>
        [Route("/api/project-management/projects/{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var projectResult = await this._projectService.DeleteProjectAsync(id);

            if (projectResult.IsFailed)
            {
                return Problem(
                        projectResult.Errors.First().Message,
                        null,
                        StatusCodes.Status404NotFound,
                        "Project could not be deleted.");
            }

            return NoContent();
        }
    }
}

using FluentValidation;
using ProjectManagement.Api.Contracts.Projects;
using ProjectManagement.Domain.Projects;

namespace ProjectManagement.Api.Validators
{
    /// <summary>
    /// Represents a validator for the <see cref="UpdateProjectRequest"/>.
    /// </summary>
    public sealed class UpdateProjectRequestValidator
        : AbstractValidator<UpdateProjectRequest>
    {
        /// <summary>
        /// Instanziates a new instance of the <see cref="UpdateProjectRequestValidator"/>>.
        /// </summary>
        public UpdateProjectRequestValidator() 
        {
            RuleFor(r => r)
                .NotEmpty()
                .WithMessage("Request is empty.");

            RuleFor(r => r.Name)
                .MaximumLength(255)
                .When(r => r.Name is not null);

            RuleFor(r => r.Description)
                .MaximumLength(1000)
                .When(r => r.Description is not null);

            RuleFor(r => r.Status)
                .Must(s => Enum.TryParse<ProjectStatus>(s, true, out _))
                .When(r => r.Status is not null)
                .WithMessage("Status must be a valid project status.");
        }
    }
}

using FluentValidation;
using ProjectManagement.Api.Contracts.Projects;
using ProjectManagement.Domain.Projects;

namespace ProjectManagement.Api.Validators
{
    /// <summary>
    /// Represents a validator for the <see cref="CreateProjectRequest"/>.
    /// </summary>
    public sealed class CreateProjectRequestValidator
        : AbstractValidator<CreateProjectRequest>
    {
        /// <summary>
        /// Instanziates a new instance of the <see cref="CreateProjectRequestValidator"/>>.
        /// </summary>
        public CreateProjectRequestValidator()
        {
            RuleFor(r => r)
                .NotEmpty()
                .WithMessage("Request is empty.");

            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("Name is empty.")
                .MaximumLength(255)
                .WithMessage("Name is too long.");


            RuleFor(r => r.Description)
                .NotEmpty()
                .WithMessage("Description is empty.")
                .MaximumLength(1000)
                .WithMessage("Description is too long.");

            RuleFor(r => r.Status)
                .NotEmpty()
                .WithMessage("Status is empty.")
                .Must(s => Enum.TryParse<ProjectStatus>(s, true, out _))
                .WithMessage("Status must be a valid project status.");
        }
    }
}

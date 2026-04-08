using FluentValidation;
using ProjectManagement.Api.Contracts.Boards;

namespace ProjectManagement.Api.Validators
{
    /// <summary>
    /// Represents a validator for the <see cref="CreateBoardRequest"/>.
    /// </summary>
    public sealed class CreateBoardRequestValidator
        : AbstractValidator<CreateBoardRequest>
    {
        /// <summary>
        /// Instanziates a new instance of the <see cref="CreateBoardRequestValidator"/>>.
        /// </summary>
        public CreateBoardRequestValidator()
        {
            RuleFor(r => r)
                .NotEmpty()
                .WithMessage("Request is empty.");

            RuleFor(r => r.Title)
                .NotEmpty()
                .WithMessage("Title is empty.")
                .MaximumLength(255)
                .WithMessage("Title is too long.");

            RuleFor(r => r.ProjectId)
                .NotEmpty()
                .WithMessage("ProjectId is empty.")
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("ProjectId can not be parsed to a guid.");
        }
    }
}

using FluentValidation;
using ProjectManagement.Api.Contracts.Groups;

namespace ProjectManagement.Api.Validators
{
    /// <summary>
    /// Represents a validator for the <see cref="CreateGroupRequest"/>.
    /// </summary>
    public sealed class CreateGroupRequestValidator
        : AbstractValidator<CreateGroupRequest>
    {
        /// <summary>
        /// Instanziates a new instance of the <see cref="CreateBoardRequestValidator"/>>.
        /// </summary>
        public CreateGroupRequestValidator()
        {
            RuleFor(r => r)
                .NotEmpty()
                .WithMessage("Request is empty.");

            RuleFor(r => r.Title)
                .NotEmpty()
                .WithMessage("Title is empty.")
                .MaximumLength(255)
                .WithMessage("Title is too long.");
        }
    }
}

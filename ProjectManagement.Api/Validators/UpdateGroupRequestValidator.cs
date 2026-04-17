using FluentValidation;
using ProjectManagement.Api.Contracts.Groups;

namespace ProjectManagement.Api.Validators
{
    /// <summary>
    /// Represents a validator for the <see cref="UpdateGroupRequest"/>.
    /// </summary>
    public sealed class UpdateGroupRequestValidator
        : AbstractValidator<UpdateGroupRequest>
    {
        /// <summary>
        /// Instanziates a new instance of the <see cref="UpdateGroupRequestValidator"/>>.
        /// </summary>
        public UpdateGroupRequestValidator()
        {
            RuleFor(r => r.CurrentTitle)
                .NotEmpty()
                .WithMessage("Title is empty.");

            RuleFor(r => r.NewTitle)
                .NotEmpty()
                .When(r => r.NewTitle is not null)
                .MaximumLength(255)
                .WithMessage("Title is too long.");
        }
    }
}

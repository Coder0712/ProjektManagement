using FluentValidation;
using ProjectManagement.Api.Contracts.Boards;

namespace ProjectManagement.Api.Validators
{
    /// <summary>
    /// Represents a validator for the <see cref="UpdateBoardRequst"/>.
    /// </summary>
    public sealed class UpdateBoardRequestValidator
            : AbstractValidator<UpdateBoardRequst>
    {
        /// <summary>
        /// Instanziates a new instance of the <see cref="UpdateBoardRequestValidator"/>>.
        /// </summary>
        public UpdateBoardRequestValidator()
        {
            RuleFor(r => r)
                .NotEmpty()
                .WithMessage("Request is empty.");

            RuleFor(r => r.Title)
                .NotEmpty()
                .When(r => r.Title is not null)
                .MaximumLength(255)
                .WithMessage("Title is too long.");
        }
    }
}

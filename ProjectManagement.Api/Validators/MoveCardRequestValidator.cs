using FluentValidation;
using ProjectManagement.Api.Contracts.Cards;

namespace ProjectManagement.Api.Validators
{
    /// <summary>
    /// Represents a validator for the <see cref="MoveCardRequest"/>.
    /// </summary>
    public sealed class MoveCardRequestValidator
        : AbstractValidator<MoveCardRequest>
    {
        /// <summary>
        /// Instanziates a new instance of the <see cref="MoveCardRequestValidator"/>>.
        /// </summary>
        public MoveCardRequestValidator()
        {
            RuleFor(r => r)
                .NotEmpty()
                .WithMessage("Request is empty.");

            RuleFor(r => r.BoardId)
                .NotEmpty()
                .WithMessage("BoardId is empty.")
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("BoardId can not be parsed to a guid..");

            RuleFor(r => r.NewGroupId)
                .NotEmpty()
                .WithMessage("NewGroupId is empty.")
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("NewGroupId can not be parsed to a guid.");
        }
    }
}

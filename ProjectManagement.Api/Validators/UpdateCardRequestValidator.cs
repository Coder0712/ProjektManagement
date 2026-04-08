using FluentValidation;
using ProjectManagement.Api.Contracts.Cards;
using ProjectManagement.Domain.Cards;

namespace ProjectManagement.Api.Validators
{
    /// <summary>
    /// Represents a validator for the <see cref="UpdateCardRequest"/>.
    /// </summary>
    public sealed class UpdateCardRequestValidator
        : AbstractValidator<UpdateCardRequest>
    {
        /// <summary>
        /// Instanziates a new instance of the <see cref="UpdateCardRequestValidator"/>>.
        /// </summary>
        public UpdateCardRequestValidator()
        {
            RuleFor(r => r)
                .NotEmpty()
                .WithMessage("Request is empty.");

            RuleFor(r => r.Title)
                .MaximumLength(255)
                .When(r => r.Title is not null)
                .WithMessage("Title is too long.");

            RuleFor(r => r.Description)
                .MaximumLength(1000)
                .When(r => r.Description is not null)
                .WithMessage("Description is too long.");

            RuleFor(r => r.Effort)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(999)
                .When(r => r.Effort is not null)
                .WithMessage("Effort must be between 0 and 999.");

            RuleFor(r => r.Status)
                .Must(s => Enum.TryParse<CardStatus>(s, true, out _))
                .When(r => r.Status is not null)
                .WithMessage("Status must be a valid card status.");

            RuleFor(r => r.BoardId)
                .NotEmpty()
                .WithMessage("BoardId is empty.")
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("BoardId can not be parsed to a guid.");
        }
    }
}

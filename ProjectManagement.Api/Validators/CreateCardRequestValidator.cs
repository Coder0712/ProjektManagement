using FluentValidation;
using ProjectManagement.Api.Contracts.Cards;
using ProjectManagement.Domain.Cards;

namespace ProjectManagement.Api.Validators
{
    /// <summary>
    /// Represents a validator for the CreateCardRequest.
    /// </summary>
    public sealed class CreateCardRequestValidator
        : AbstractValidator<CreateCardRequest>
    {
        /// <summary>
        /// Instanziates a new instance of the <see cref="CreateCardRequestValidator"/>>.
        /// </summary>
        public CreateCardRequestValidator()
        {
            RuleFor(r => r)
                .NotEmpty()
                .WithMessage("Request is empty.");

            RuleFor(r => r.Title)
                .NotEmpty()
                .WithMessage("Title is empty.")
                .MaximumLength(255)
                .WithMessage("Title is too long.");

            RuleFor(r => r.Description)
                .MaximumLength(2000)
                .When(r => !string.IsNullOrEmpty(r.Description))
                .WithMessage("Description is too long.");

            RuleFor(r => r.Effort)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(999)
                .When(r => r.Effort is not null)
                .WithMessage("Effort must be between 0 and 999.");

            RuleFor(r => r.Status)
                .Must(s => Enum.TryParse<CardStatus>(s, true, out _))
                .WithMessage("Status must be a valid card status.");

            RuleFor(r => r.BoardId)
                .NotEmpty()
                .WithMessage("BoardId is empty.")
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("BoardId can not be parsed to a guid.");

            RuleFor(r => r.GroupId)
                .NotEmpty()
                .WithMessage("GroupId is empty.")
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("GroupId can not be parsed to a guid.");
        }
    }
}

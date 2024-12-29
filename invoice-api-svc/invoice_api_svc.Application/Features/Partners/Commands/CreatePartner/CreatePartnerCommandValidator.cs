using invoice_api_svc.Application.Interfaces.Repositories;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Partners.Commands.CreatePartner
{
    public class CreatePartnerCommandValidator : AbstractValidator<CreatePartnerCommand>
    {
        private readonly IUserRepositoryAsync userRepository;

        public CreatePartnerCommandValidator(IUserRepositoryAsync userRepository)
        {
            this.userRepository = userRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.CompanyName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.Address1)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync(IsEmailUnique).WithMessage("The email {PropertyValue} is already registered.");

            RuleFor(p => p.Phone)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters.");

            RuleFor(p => p.LicenseKey)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }

        private async Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken)
        {
            return !(await userRepository.EmailExistsAsync(email, cancellationToken));
        }
    }
}

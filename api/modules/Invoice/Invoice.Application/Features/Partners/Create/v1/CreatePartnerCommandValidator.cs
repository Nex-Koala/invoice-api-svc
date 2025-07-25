using FluentValidation;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Create.v1;

public class CreatePartnerCommandValidator : AbstractValidator<CreatePartnerCommand>
{
    public CreatePartnerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage("Company Name is required.")
            .MaximumLength(200).WithMessage("Company Name must not exceed 200 characters.");

        RuleFor(x => x.Address1)
            .NotEmpty().WithMessage("Address Line 1 is required.")
            .MaximumLength(255).WithMessage("Address Line 1 must not exceed 255 characters.");

        RuleFor(x => x.Address2)
            .MaximumLength(255).WithMessage("Address Line 2 must not exceed 255 characters.");

        RuleFor(x => x.Address3)
            .MaximumLength(255).WithMessage("Address Line 3 must not exceed 255 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email Address is required.")
            .EmailAddress().WithMessage("Invalid Email Address format.")
            .MaximumLength(150).WithMessage("Email must not exceed 150 characters.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone Number is required.")
            .Matches(@"^\+?\d{7,15}$").WithMessage("Invalid Phone Number format.")
            .MaximumLength(20).WithMessage("Phone Number must not exceed 20 characters.");

        RuleFor(x => x.LicenseKey.MaxSubmissions)
            .GreaterThan(0).WithMessage("Max Submissions must be greater than 0.");
    }
}


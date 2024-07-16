using Api.DTO;
using Domain;
using Domain.Entity;
using FluentValidation;

namespace Api.Validations;

public class PasswordValidation : AbstractValidator<Password>
{
    public PasswordValidation()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required");
        RuleFor(p => p.Value)
            .NotEmpty()
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
        When(p => p.Type == PasswordType.ForEmail, () => 
        {
            RuleFor(p => p.Name)
                .EmailAddress().WithMessage("Invalid email address");
        });
        
    }
}

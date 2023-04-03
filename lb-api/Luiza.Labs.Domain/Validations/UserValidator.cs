using FluentValidation;
using Luiza.Labs.Domain.Models;

namespace Luiza.Labs.Domain.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .Length(2, 50);
            
            RuleFor(x => x.EmailAddress)
                .NotNull()
                .EmailAddress();

            RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                    .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[!@#$%^&*()_+~`|}{[\]:;?/.,<>]+").WithMessage("Your password must contain at least one char special.");

        }
    }
}

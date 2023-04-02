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

            RuleFor(x => x.Password)
                .NotEmpty();
                
        }
    }
}

using FluentValidation;
using Luiza.Labs.Domain.Models;

namespace Luiza.Labs.Domain.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .Length(0, 10);
            
            RuleFor(x => x.EmailAdress)
                .NotNull()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
                
        }
    }
}

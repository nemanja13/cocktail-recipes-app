using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using Implementation.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginRequestValidator(CocktailRecipesContext context)
        {
            RuleFor(x => x.Username)
               .NotEmpty()
               .WithMessage("Username must not be empty")
               .DependentRules(() => {
                   RuleFor(x => x.Username)
                   .Must(username => context.Users.Any(u => u.Username == username))
                   .WithMessage("User with an username of {PropertyValue} does not exist in database");
               });
            RuleFor(x => x.Password)
               .NotEmpty()
               .WithMessage("Password must not be empty")
               .DependentRules(() => {
                   RuleFor(x => x.Password)
                   .Must(password => context.Users.Any(u => u.Password == password.MD5Encrypt()))
                   .WithMessage("User with this password does not exist in database");
               });
        }
    }
}

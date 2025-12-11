using Ecommerce.Dtos.User;
using FluentValidation;

namespace Ecommerce.Validations.User
{
    public class RegisterValidation : AbstractValidator<RegisterDto>
    {
        public RegisterValidation()
        {

            RuleFor(u => u.email)
                .EmailAddress().NotEmpty().NotNull().WithMessage("please write a valid email");
            RuleFor(u => u.username)
                .NotNull().NotEmpty().Length(8, 20)
                .WithMessage("username must have between 8 and 20 letters");
            RuleFor(u => u.password)
                .NotNull().NotEmpty().MinimumLength(12)
                .WithMessage("password must have 12 letters");
            RuleFor(u => u.role)
                .NotNull().IsInEnum();
        }
    }
}

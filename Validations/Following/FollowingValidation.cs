using Ecommerce.Dtos.Followers;
using FluentValidation;

namespace Ecommerce.Validations.Following
{
    public class FollowingValidation : AbstractValidator<FollowDto>
    {
        public FollowingValidation()
        {

            RuleFor(f => f.userId)
                .NotEmpty().NotNull();
            RuleFor(f => f.followerId)
                .NotNull().NotEmpty();

        }


    }
}

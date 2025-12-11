using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.WishLists;
using FluentValidation;

namespace Ecommerce.Validations.WishLists
{
    public class AddToWishListsValidation : AbstractValidator<AddItemDto>
    {
        public AddToWishListsValidation()
        {
            RuleFor(i => i.userId).NotNull().NotEmpty();
            RuleFor(i => i.ProductId).NotNull().GreaterThan(0);
        }
    }
}
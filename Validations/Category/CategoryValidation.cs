using Ecommerce.Dtos.Category;
using FluentValidation;

namespace Ecommerce.Validations.Category
{
    public class CategoryValidation : AbstractValidator<CreateCategoryDto>
    {
        public CategoryValidation() {

            RuleFor(c => c.Name)
                .NotEmpty().NotNull().WithMessage("Category name should'nt be empty or null");
        
        }
    }
}

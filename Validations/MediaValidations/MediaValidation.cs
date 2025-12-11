using Ecommerce.Dtos.MediaDtos;
using FluentValidation;

namespace Ecommerce.Validations.Media
{
    public class MediaValidation : AbstractValidator<CreateFile>
    {
        public MediaValidation()
        {

            RuleFor(f => f.file).NotEmpty().NotNull()
                .WithMessage("file path extension must be in a valid format!");

        }
    }
}

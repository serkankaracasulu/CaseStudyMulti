using FluentValidation;
using ProductAPI.Entities.Products;
using ProductAPI.Models.DTOs.Products;

namespace ProductAPI.Applicaitons.Products
{
    public class ProductCreateValidation : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateValidation()
        {
            RuleFor(d => d.Description).NotEmpty().MaximumLength(ProductConsts.DescriptionMaxLength).MinimumLength(2);
        }
    }
}

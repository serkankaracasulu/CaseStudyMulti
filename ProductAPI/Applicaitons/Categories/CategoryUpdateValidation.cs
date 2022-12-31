using FluentValidation;
using ProductAPI.Entities.Products.Categories;
using ProductAPI.Models.DTOs.Categories;

namespace ProductAPI.Applicaitons.Categories
{
    public class CategoryUpdateValidation : AbstractValidator<CategoryCreateDto>
    {
        public CategoryUpdateValidation()
        {
            RuleFor(d => d.Name).NotEmpty().MaximumLength(CategoryConsts.NameMaxLength).MinimumLength(2);
        }
    }
}

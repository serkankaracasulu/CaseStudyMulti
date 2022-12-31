using FluentValidation;
using ProductAPI.Entities.ProductImages;
using ProductAPI.Entities.Products.Categories;

namespace ProductAPI.Applicaitons.Images
{
    public class ImageUpdateValidation : AbstractValidator<ProductImage>
    {
        public ImageUpdateValidation()
        {
        }
    }
}

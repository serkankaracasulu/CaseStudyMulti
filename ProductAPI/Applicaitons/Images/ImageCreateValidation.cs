using FluentValidation;
using ProductAPI.Entities.ProductImages;
using ProductAPI.Entities.Products.Categories;

namespace ProductAPI.Applicaitons.Images
{
    public class ImageCreateValidation : AbstractValidator<ProductImage>
    {
        public ImageCreateValidation()
        {
        }
    }
}

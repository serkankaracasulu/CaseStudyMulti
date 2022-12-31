namespace ProductAPI.Models.DTOs.Images
{
    public class ProductImagerCreateDto
    {
        public string ProductId { get; set; }
        public IFormFile Image { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using ProductAPI.Common.Mapping;
using Redis.OM.Modeling;

namespace ProductAPI.Entities.ProductImages
{
    [Document(StorageType = StorageType.Json, Prefixes = new[] { nameof(ProductImage) })]
    public class ProductImage : IMapFrom<ProductImage>
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public byte[] Image { get; set; }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Common.Model;
using ProductAPI.Entities.ProductImages;
using ProductAPI.Models.DTOs.Images;
using System.Net;

namespace productImageAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly ILogger<ImagesController> _logger;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;

        public ImagesController(ILogger<ImagesController> logger, IProductImageRepository productImageRepository, IMapper mapper)
        {
            _productImageRepository = productImageRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<ProductImage>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> List([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                return Ok(await _productImageRepository.List<ProductImage>(_mapper.ConfigurationProvider, pageNumber, pageSize));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductImage), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var productImage = await _productImageRepository.Get(id);
                if (productImage == null) return NotFound(id);
                return Ok(_mapper.Map<ProductImage>(productImage));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var productImage = await _productImageRepository.Get(id);
                if (productImage == null) return NotFound(id);
                _productImageRepository.Delete(productImage);
                var result = await _productImageRepository.Save();
                if (result == 0)
                    return BadRequest();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [ProducesResponseType(typeof(ProductImage), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromForm] ProductImagerCreateDto productImage)
        {
            try
            {
                using var stream = new MemoryStream();
                await productImage.Image.CopyToAsync(stream);
                var newproductImage = new ProductImage() { ProductId = productImage.ProductId, Image = stream.ToArray(), Id = Guid.NewGuid().ToString() };
                newproductImage.Id = Guid.NewGuid().ToString();
                newproductImage = await _productImageRepository.Insert(newproductImage);
                var result = await _productImageRepository.Save();
                if (result == 0)
                    return BadRequest();
                return Ok(_mapper.Map<ProductImage>(newproductImage));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductImage), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(string id, [FromBody] ProductImage dto)
        {
            try
            {
                var productImage = await _productImageRepository.Get(id);
                if (productImage == null) return NotFound(id);
                var updatedproductImage = _productImageRepository.Update(_mapper.Map(dto, productImage));
                var result = await _productImageRepository.Save();
                if (result == 0)
                    return BadRequest();
                return Ok(_mapper.Map<ProductImage>(updatedproductImage));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
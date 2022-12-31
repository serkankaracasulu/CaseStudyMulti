using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Common.Model;
using ProductAPI.Entities.Categories;
using ProductAPI.Entities.ProductImages;
using ProductAPI.Entities.Products;
using ProductAPI.Models.DTOs.Categories;
using ProductAPI.Models.DTOs.Products;
using System.Net;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;

        public ProductsController(ILogger<ProductsController> logger, IProductRepository productRepository, IMapper mapper, ICategoryRepository categoryRepository,IProductImageRepository productImageRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            this._productImageRepository = productImageRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<ProductDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> List([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                return Ok(await _productRepository.List<ProductDto>(_mapper.ConfigurationProvider, pageNumber, pageSize));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}/categories")]
        [ProducesResponseType(typeof(PaginatedList<CategoryDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CategoryList(string id, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                return Ok(await _categoryRepository.List<CategoryDto>(_mapper.ConfigurationProvider, id, pageNumber, pageSize));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}/images")]
        [ProducesResponseType(typeof(PaginatedList<ProductImage>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ProductImageList(string id, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                return Ok(await _productImageRepository.List<ProductImage>(_mapper.ConfigurationProvider, id, pageNumber, pageSize));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var product = await _productRepository.Get(id);
                if (product == null) return NotFound(id);
                return Ok(_mapper.Map<ProductDto>(product));
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
                var product = await _productRepository.Get(id);
                if (product == null) return NotFound(id);
                _productRepository.Delete(product);
                var result = await _productRepository.Save();
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
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto product)
        {
            try
            {
                var p = _mapper.Map<Product>(product);
                p.Id = Guid.NewGuid().ToString();
                var newProduct = await _productRepository.Insert(p);
                var result = await _productRepository.Save();
                if (result == 0)
                    return BadRequest();
                return Ok(_mapper.Map<ProductDto>(newProduct));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(string id, [FromBody] ProductUpdateDto dto)
        {
            try
            {
                var product = await _productRepository.Get(id);
                if (product == null) return NotFound(id);
                var updatedProduct = _productRepository.Update(_mapper.Map(dto, product));
                var result = await _productRepository.Save();
                if (result == 0)
                    return BadRequest();
                return Ok(_mapper.Map<ProductDto>(updatedProduct));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
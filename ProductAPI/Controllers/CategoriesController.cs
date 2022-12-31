using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Common.Model;
using ProductAPI.Entities.Categories;
using ProductAPI.Models.DTOs.Categories;
using System.Net;

namespace categoryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ILogger<CategoriesController> logger, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<CategoryDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> List([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                return Ok(await _categoryRepository.List<CategoryDto>(_mapper.ConfigurationProvider, pageNumber, pageSize));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var category = await _categoryRepository.Get(id);
                if (category == null) return NotFound(id);
                return Ok(_mapper.Map<CategoryDto>(category));
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
                var category = await _categoryRepository.Get(id);
                if (category == null) return NotFound(id);
                _categoryRepository.Delete(category);
                var result = await _categoryRepository.Save();
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
        [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto category)
        {
            try
            {
                var newcategory = _mapper.Map<Category>(category);
                newcategory.Id = Guid.NewGuid().ToString();
                newcategory = await _categoryRepository.Insert(newcategory);
                var result = await _categoryRepository.Save();
                if (result == 0)
                    return BadRequest();
                return Ok(_mapper.Map<CategoryDto>(newcategory));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(string id, [FromBody] CategoryUpdateDto dto)
        {
            try
            {
                var category = await _categoryRepository.Get(id);
                if (category == null) return NotFound(id);
                var updatedcategory = _categoryRepository.Update(_mapper.Map(dto, category));
                var result = await _categoryRepository.Save();
                if (result == 0)
                    return BadRequest();
                return Ok(_mapper.Map<CategoryDto>(updatedcategory));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
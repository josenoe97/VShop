using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using VShop.ProductAPI.DTOs;
using VShop.ProductAPI.Roles;
using VShop.ProductAPI.Services.Interface;

namespace VShop.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase 
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get() 
        {
            var categoriesDto = await _categoryService.GetCategoriesAsync();

            if(categoriesDto is null)
                return NotFound("Categories not found.");

            return Ok(categoriesDto);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoryProducts()
        {
            var categoriesDto = await _categoryService.GetCategoriesProductsAsync();

            if (categoriesDto is null)
                return NotFound("Categories not found.");

            return Ok(categoriesDto);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var categoryDto = await _categoryService.GetCategoryByIdAsync(id);

            if (categoryDto is null)
                return NotFound("Category not found.");

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO is null)
                return BadRequest("Invalid Data");

            await _categoryService.AddCategoryAsync(categoryDTO);

            return new CreatedAtRouteResult("GetCategory", new { id = categoryDTO.CategoryId},
                categoryDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
                return BadRequest();

            if (categoryDTO is null)
                return BadRequest();

            await _categoryService.UpdateCategoryAsync(categoryDTO);

            return Ok(categoryDTO);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var categoryDto = await _categoryService.GetCategoryByIdAsync(id);

            if (categoryDto is null)
                return NotFound("Category not found.");

            await _categoryService.RemoveCategoryAsync(id);

            return Ok(categoryDto);
        }
    }
}

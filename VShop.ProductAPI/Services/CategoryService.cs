using AutoMapper;
using VShop.ProductAPI.DTOs;
using VShop.ProductAPI.Models;
using VShop.ProductAPI.Repositories.Interface;
using VShop.ProductAPI.Services.Interface;

namespace VShop.ProductAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        {
            var categoriesEntity = await _categoryRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesProductsAsync()
        {
            var categoriesEntity = await _categoryRepository.GetCategoriesProductsAsync();

            return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            var categoriesEntity = await _categoryRepository.GetByIdAsync(id);

            return _mapper.Map<CategoryDTO>(categoriesEntity);
        }

        public async Task AddCategoryAsync(CategoryDTO categoryDTO)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDTO);

            await _categoryRepository.CreateAsync(categoryEntity);

            categoryDTO.CategoryId = categoryEntity.CategoryId;
        }

        public async Task UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDTO);

            await _categoryRepository.UpdateAsync(categoryEntity);
        }

        public async Task RemoveCategoryAsync(int id)
        {
            var categoryEntity = _categoryRepository.GetByIdAsync(id).Result;

            await _categoryRepository.DeleteAsync(categoryEntity.CategoryId);
        }
    }
}

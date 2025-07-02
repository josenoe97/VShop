using AutoMapper;
using VShop.ProductAPI.DTOs;
using VShop.ProductAPI.Models;
using VShop.ProductAPI.Repositories.Interface;
using VShop.ProductAPI.Services.Interface;

namespace VShop.ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var productsEntity = await _productRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var productEntity = await _productRepository.GetByIdAsync(id);

            return _mapper.Map<ProductDTO>(productEntity);
        }

        public async Task AddProductAsync(ProductDTO productDTO)
        {
            var productEntity = _mapper.Map<Product>(productDTO);

            await _productRepository.CreateAsync(productEntity);

            productDTO.Id = productEntity.Id;
        }

        public async Task UpdateProductAsync(ProductDTO productDTO)
        {
            var productEntity = _mapper.Map<Product>(productDTO);

            await _productRepository.UpdateAsync(productEntity);
        }
        
        public async Task RemoveProductAsync(int id)
        {
            var productEntity = await _productRepository.GetByIdAsync(id);

            await _productRepository.DeleteAsync(productEntity.Id);
        }
    }
}

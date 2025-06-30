using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string apiEndpoint = "/api/products/";
        private readonly JsonSerializerOptions _options;
        private ProductsViewModel productViewModel;
        private IEnumerable<ProductsViewModel> productsViewModel;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public Task<IEnumerable<ProductsViewModel>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<ProductsViewModel> FindProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductsViewModel> CreateProduct(ProductsViewModel productViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<ProductsViewModel> UpdateProduct(ProductsViewModel productsViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

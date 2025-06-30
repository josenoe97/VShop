using VShop.Web.Models;

namespace VShop.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductsViewModel>> GetAllProducts();
        Task<ProductsViewModel> FindProductById(int id);
        Task<ProductsViewModel> CreateProduct(ProductsViewModel productViewModel);
        Task<ProductsViewModel> UpdateProduct(ProductsViewModel productsViewModel);
        Task<bool> DeleteProductById(int id);
    }
}

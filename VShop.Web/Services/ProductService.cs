﻿using System.Runtime.InteropServices;
using System.Text;
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

        private ProductViewModel productViewModel;
        private IEnumerable<ProductViewModel> productsViewModel;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            var client = _httpClientFactory.CreateClient("ProductAPI");

            using (var response = await client.GetAsync(apiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    productsViewModel = await JsonSerializer
                        .DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _options);
                }
                else
                {
                    return null;
                }

            }

            return productsViewModel;
        }

        public async Task<ProductViewModel> FindProductById(int id)
        {
            var client = _httpClientFactory.CreateClient("ProductAPI");

            using (var response = await client.GetAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    var productViewModel = await JsonSerializer
                        .DeserializeAsync<ProductViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }

            return productViewModel;
        }

        public async Task<ProductViewModel> CreateProduct(ProductViewModel productViewModel)
        {
            var client = _httpClientFactory.CreateClient("ProductAPI");

            StringContent content = new StringContent(JsonSerializer.Serialize(productViewModel),
                                    Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    productViewModel = await JsonSerializer
                        .DeserializeAsync<ProductViewModel>(apiResponse, _options); //
                }
                else
                {
                    return null;
                }

                return productViewModel;
            }
        }

        public async Task<ProductViewModel> UpdateProduct(ProductViewModel productsViewModel)
        {
            var client = _httpClientFactory.CreateClient("ProductAPI");

            ProductViewModel productUpdated = new ProductViewModel();

            using (var response = await client.PutAsJsonAsync(apiEndpoint, productViewModel))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    productUpdated = await JsonSerializer
                        .DeserializeAsync<ProductViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }

                return productUpdated;
            }
        }

        public async Task<bool> DeleteProductById(int id)
        {
            var client = _httpClientFactory.CreateClient("ProductAPI");

            using (var response = await client.DeleteAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

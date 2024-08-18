using System.Net.Http;
using Newtonsoft.Json;
using CryplexAdmin.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Text;
using System.ComponentModel;

namespace CryplexAdmin.Services
{
    public class ProductService
    {
        private readonly HttpClientService _httpClientService;

        public ProductService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClientService.GetAsync("products");
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();

                var productResponseList = JsonConvert.DeserializeObject<List<ProductResponse>>(content);

                return productResponseList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GetProductDataAsync(string productId)
        {
            try
            {
                HttpResponseMessage response = await _httpClientService.GetAsync($"products/{productId}/product.dat");
                response.EnsureSuccessStatusCode();
                string productData = await response.Content.ReadAsStringAsync();

                return productData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null; 
            }
        }

        public async Task DeleteProductAsync(string productId)
        {
            try
            {
                HttpResponseMessage response = await _httpClientService.DeleteAsync($"products/{productId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public async Task CreateProductAsync(ProductRequest product)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(product);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClientService.PostAsync("products", content);

                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HTTP Request error: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
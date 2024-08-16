using CryplexAdmin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryplexAdmin.Services
{
    public class LicenseService
    {
        private readonly HttpClientService _httpClientService;

        public LicenseService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<List<License>> GetAllLicensesAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClientService.GetAsync("licenses");
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();

                var licenseList = JsonConvert.DeserializeObject<List<License>>(content);

                return licenseList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task CreateLicenseAsync(string productId)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(new { productId });

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClientService.PostAsync("licenses", content);

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("License created successfully. Response: " + responseBody);
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

        public async Task DeleteLicenseAsync(string licenseId)
        {
            try
            {
                HttpResponseMessage response = await _httpClientService.DeleteAsync($"licenses/{licenseId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}

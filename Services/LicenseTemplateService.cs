using CryplexAdmin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryplexAdmin.Services
{
    internal class LicenseTemplateService
    {

        private readonly HttpClientService _httpClientService;

        public LicenseTemplateService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<List<LicenseTemplate>> GetAllLicenseTemplatesAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClientService.GetAsync("products");
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();

                var productResponseList = JsonConvert.DeserializeObject<List<LicenseTemplate>>(content);

                return productResponseList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}

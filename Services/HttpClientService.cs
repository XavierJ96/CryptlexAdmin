using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace CryplexAdmin.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;
        private const string BaseAddress = "https://api.cryptlex.com/v3/";
        
        //Bearer Token
        private const string BearerToken = "";

        public HttpClientService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };

            SetBearerToken(BearerToken);
        }

        public void SetBearerToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            return await _httpClient.GetAsync(endpoint);
        }

        public async Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent content)
        {
            return await _httpClient.PostAsync(endpoint, content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, endpoint);

            return await _httpClient.SendAsync(request);
        }
    }
}

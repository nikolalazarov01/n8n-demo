using System.Net.Http;
using System.Threading.Tasks;

namespace Project.Services
{
    public class N8nService
    {
        private readonly HttpClient _httpClient;

        public N8nService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> LogActionAsync()
        {
            // Replace with your actual n8n webhook URL
            var url = "https://your-n8n-instance.com/webhook/log-action";

            var response = await _httpClient.GetAsync(url); // Assuming GET, change to PostAsJsonAsync if needed
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
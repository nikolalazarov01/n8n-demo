using System.Net.Http;
using System.Text;
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

        public async Task<string> LogActionAsync(string text)
        {
            // Replace with your actual n8n webhook URL
            var url = "http://localhost:5678/webhook-test/execute-workflow";

            var json = $"{{\"text\":\"{text.Replace("\"", "\\\"")}\"}}"; // Escape quotes
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
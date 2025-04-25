using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KarriereAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CareerAdviceController : ControllerBase
    {
        private readonly HttpClient _httpClient = new HttpClient();

        [HttpGet("ask")]
        public async Task<IActionResult> Ask([FromQuery] string question)
        {
            var apiKey = "r3BqhkPbavOzjEBDnXIxt0lN7JeDLHsSn82FZxPx";

            var payload = new
            {
                model = "command",
                prompt = question,
                max_tokens = 150,
                temperature = 0.7
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.cohere.ai/v1/generate");
            request.Headers.Add("Authorization", $"Bearer {apiKey}");
            request.Headers.Add("Cohere-Version", "2022-12-06");

            request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            return Content(responseBody, "application/json");
        }
    }
}

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ChatbotService.Models;
using Microsoft.Extensions.Options;

namespace ChatbotService.Services
{
    public class AzureFoundryChatService : IChatbotService
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly HttpClient _httpClient;
        private readonly AzureAIFoundryOptions _options;
        private readonly ILogger<AzureFoundryChatService> _logger;

        public AzureFoundryChatService(
            HttpClient httpClient,
            IOptions<AzureAIFoundryOptions> options,
            ILogger<AzureFoundryChatService> logger)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _logger = logger;
        }

        public async Task<ChatbotResponse> SendAsync(ChatbotRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                throw new ArgumentException("Message is required.", nameof(request));
            }

            if (string.IsNullOrWhiteSpace(_options.ChatCompletionsUrl) || string.IsNullOrWhiteSpace(_options.ApiKey))
            {
                throw new InvalidOperationException("Azure AI Foundry configuration is missing. Set AzureAIFoundry:ChatCompletionsUrl and AzureAIFoundry:ApiKey.");
            }

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, _options.ChatCompletionsUrl);
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequest.Headers.Add("api-key", _options.ApiKey);

            var payload = new
            {
                messages = BuildMessages(request),
                temperature = _options.Temperature,
                max_tokens = _options.MaxTokens
            };

            var json = JsonSerializer.Serialize(payload, JsonOptions);
            httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Azure AI Foundry call failed. Status: {StatusCode}, Response: {ResponseBody}", response.StatusCode, responseBody);
                throw new InvalidOperationException($"Azure AI Foundry request failed with status code {(int)response.StatusCode}.");
            }

            using var document = JsonDocument.Parse(responseBody);
            var root = document.RootElement;

            var choice = root.GetProperty("choices")[0];
            var message = choice.GetProperty("message").GetProperty("content").GetString() ?? string.Empty;
            var finishReason = choice.TryGetProperty("finish_reason", out var reasonEl)
                ? reasonEl.GetString()
                : null;
            var model = root.TryGetProperty("model", out var modelEl)
                ? modelEl.GetString()
                : null;

            return new ChatbotResponse(message, request.SessionId, model, finishReason);
        }

        private static object[] BuildMessages(ChatbotRequest request)
        {
            var messages = new List<object>();

            if (!string.IsNullOrWhiteSpace(request.SystemPrompt))
            {
                messages.Add(new { role = "system", content = request.SystemPrompt });
            }

            messages.Add(new { role = "user", content = request.Message });
            return messages.ToArray();
        }
    }
}

namespace SampleWebApplication1.Models
{
    public record ChatbotRequest(string Message, string? SessionId = null, string? SystemPrompt = null);

    public record ChatbotResponse(string Reply, string? SessionId = null, string? Model = null, string? FinishReason = null);

    public class AzureAIFoundryOptions
    {
        public const string SectionName = "AzureAIFoundry";

        public string ChatCompletionsUrl { get; set; } = string.Empty;

        public string ApiKey { get; set; } = string.Empty;

        public double Temperature { get; set; } = 0.7;

        public int MaxTokens { get; set; } = 500;
    }
}

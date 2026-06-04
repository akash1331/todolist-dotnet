using SampleWebApplication1.Models;

namespace SampleWebApplication1.Services
{
    public interface IChatbotService
    {
        Task<ChatbotResponse> SendAsync(ChatbotRequest request, CancellationToken cancellationToken = default);
    }
}

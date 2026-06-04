using ChatbotService.Models;

namespace ChatbotService.Services
{
    public interface IChatbotService
    {
        Task<ChatbotResponse> SendAsync(ChatbotRequest request, CancellationToken cancellationToken = default);
    }
}

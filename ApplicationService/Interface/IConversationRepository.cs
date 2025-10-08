
namespace EFCore;

public interface IConversationRepository
{
    Task DeleteConversationAsync(int senderId, int receiverId);
}
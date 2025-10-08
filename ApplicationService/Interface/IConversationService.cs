namespace ApplicationService.Interface;

public interface IConversationService
{
    Task DeleteConversationAsync(int senderId, int receiverId);
}
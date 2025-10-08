using ApplicationService.Interface;
using EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService;
public class ConversationService : IConversationService
{
    private readonly IConversationRepository _conversationRepository;

    public ConversationService(IConversationRepository conversationRepository)
    {
        _conversationRepository = conversationRepository;
    }

    public async Task DeleteConversationAsync(int senderId, int receiverId)
    {
        await _conversationRepository.DeleteConversationAsync(receiverId, senderId);
    }
}

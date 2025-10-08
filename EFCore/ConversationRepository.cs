using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore;
public class ConversationRepository : IConversationRepository
{
    private readonly MyDbContext _context;

    public ConversationRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task DeleteConversationAsync(int senderId, int receiverId)
    {
        var chatt = await _context.Chatt.Where(c => c.ReceiverId == receiverId && c.SenderId == senderId || c.ReceiverId == senderId && c.SenderId == receiverId).FirstOrDefaultAsync();

        if (chatt != null)
        {
            _context.Chatt.Remove(chatt);
        }

        await _context.SaveChangesAsync();
    }
}

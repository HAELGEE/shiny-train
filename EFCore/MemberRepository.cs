using Entity;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;


namespace EFCore;
public class MemberRepository : IMemberRepository
{
    private readonly MyDbContext _dbContext;

    public MemberRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Member>> GetAllMembersAsync() => await _dbContext.Member.OrderBy(m => m.UserName).ToListAsync();
    public async Task<Member> GetOneMemberAsync(int id) => await _dbContext.Member.Where(m => m.Id == id).Include(m => m.MemberViews).SingleOrDefaultAsync();
    public async Task<Member> GetMemberByUsernameAsync(string userName) => await _dbContext.Member.Where(m => m.UserName == userName).Include(m => m.MemberViews).SingleOrDefaultAsync();
    public async Task<List<Member>> GetAdminMembersAsync() => await _dbContext.Member.Where(m => m.IsAdmin == true).ToListAsync();
    public async Task<Member> GetAdminMemberAsync(int id) => await _dbContext.Member.Where(m => m.IsAdmin == true && m.Id == id).SingleOrDefaultAsync();
    public async Task<Member> GetMemberByEmailAsync(string email) => await _dbContext.Member.Where(m => m.Email == email).Include(m => m.MemberViews).SingleOrDefaultAsync();
    public async Task<Member> GetMemberByUsernamePasswordAsync(string username, string password) => await _dbContext.Member.Where(m => m.UserName == username && m.Password == password).FirstOrDefaultAsync();
    public async Task UpdateMemberAsync(Member member)
    {
        member.PasswordValidation = "";
        _dbContext.Member.Update(member);
        await _dbContext.SaveChangesAsync();        
    }
    public async Task UpdateMemberAdminrightsAsync(int id, bool isAdmin)
    {
        var member = await GetOneMemberAsync(id);

        member.IsAdmin = isAdmin;
        
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteMemberAsync(Member member)
    {
        _dbContext.Remove(member);
        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateMemberAsync(Member member)
    {
        member.PasswordValidation = "";
        _dbContext.Add(member);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateReportsForMemberAsync(int id)
    {
        var member = new Member();
        member = await GetOneMemberAsync(id);

        member.Reports++;

        await _dbContext.SaveChangesAsync();
    }
    public async Task UpdateProfilePostCounterAsync(int id)
    {
        var member = new Member();
        member = await GetOneMemberAsync(id);

        member.TotalPosts++;

        await _dbContext.SaveChangesAsync();
    }
    public async Task UpdateProfileSubReplyCounterAsync(int id)
    {
        var member = new Member();
        member = await GetOneMemberAsync(id);

        member.TotalReply++;

        await _dbContext.SaveChangesAsync();
    }
    public async Task AddProfileViewAsync(int visitorId, int userId)
    {
        var memberViewList = await _dbContext.MemberViews.ToListAsync();

        bool checkIfTrue = false;

        if (memberViewList != null)
        {
            foreach (var view in memberViewList)
            {
                if (view.MemberId == userId && view.VisitorId == visitorId)
                {
                    checkIfTrue = true; break;
                }
            }
        }

        if (!checkIfTrue)
        {
            var memberView = new MemberView
            {
                MemberId = userId,
                VisitorId = visitorId,
            };

            _dbContext.MemberViews.Add(memberView);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task ViewProfileViewsAsync(int id) => await _dbContext.MemberViews.Where(m => m.MemberId == id).ToListAsync();


    // Chatt CRUD
   

    public async Task<List<Chatt>> GetAllChattUsersAsync(int id)
    {
        
        var latestChatIds = await _dbContext.Chatt
            .Where(c => c.SenderId == id)
            .GroupBy(c => c.ReceiverId)
            .Select(g => g
                .OrderByDescending(c => c.TimeCreated)
                .FirstOrDefault().Id)
            .ToListAsync();

       
        return await _dbContext.Chatt
            .Where(c => latestChatIds.Contains(c.Id))
            .Include(c => c.ReceiverMember)
            .ToListAsync();
    }

    public async Task<List<Chatt>> GetAllChattReceiversAsync(int id)
    {

        var latestChatIds = await _dbContext.Chatt
            .Where(c => c.ReceiverId == id)
            .GroupBy(c => c.SenderId)
            .Select(g => g
                .OrderByDescending(c => c.TimeCreated)
                .FirstOrDefault().Id)
            .ToListAsync();


        return await _dbContext.Chatt
            .Where(c => latestChatIds.Contains(c.Id))
            .Include(c => c.SenderMember)
            .ToListAsync();
    }

    public async Task<List<Chatt>> AllChatForMemberAsync(int userId)
    {
        var allSent = await GetAllChattUsersAsync(userId);
        var allReceived = await GetAllChattReceiversAsync(userId);

        var allChats = allSent.Concat(allReceived)
            .OrderBy(c => c.TimeCreated).ToList();

        return allChats;
    }

    public async Task<List<Chatt>> GetAllChattMessagesFromReceiverIdAsync(int userId, int receiverId) => await _dbContext.Chatt
        .Where(c => c.ReceiverId == receiverId && c.SenderId == userId || c.ReceiverId == userId && c.SenderId == receiverId)        
        .Order()
        .ToListAsync();
    
    public async Task CreateChattWithUserAsync(Chatt chatt)
    {
        chatt.TimeCreated = DateTime.Now;
        _dbContext.Add(chatt);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteChattWithUserAsync(int userId, int receiverId)
    {
        var chatts = await GetAllChattMessagesFromReceiverIdAsync(userId, receiverId);

        foreach (var chatt in chatts)
        {
            _dbContext.Remove(chatt);
        }
        await _dbContext.SaveChangesAsync();
    }

}

using Entity;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace EFCore;
public class MemberRepository : IMemberRepository
{
    private readonly MyDbContext _dbContext;

    public MemberRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Member>> GetAllMembersAsync() => await _dbContext.Member.OrderBy(m => m.UserName).ToListAsync();
    public async Task<Member> GetOneMemberAsync(int id) => await _dbContext.Member.Where(m => m.Id == id).SingleOrDefaultAsync();
    public async Task<Member> GetMemberByUsernameAsync(string userName) => await _dbContext.Member.Where(m => m.UserName == userName).SingleOrDefaultAsync();
    public async Task<List<Member>> GetAdminMembersAsync() => await _dbContext.Member.Where(m => m.IsAdmin == true).ToListAsync();
    public async Task<Member> GetAdminMemberAsync(int id) => await _dbContext.Member.Where(m => m.IsAdmin == true && m.Id == id).SingleOrDefaultAsync();
    public async Task<Member> GetMemberByEmailAsync(string email) => await _dbContext.Member.Where(m => m.Email == email).SingleOrDefaultAsync();
    public async Task<Member> GetMemberByUsernamePasswordAsync(string username, string password) => await _dbContext.Member.Where(m => m.UserName == username && m.Password == password).FirstOrDefaultAsync();
    public async Task UpdateMemberAsync(Member member)
    {       
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
        _dbContext.Add(member);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateProfileViewsAsync(int id)
    {
        var member = new Member();
        member = await GetOneMemberAsync(id);

        member.ProfileViews++;

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
    public async Task UpdateProfileReplyCounterAsync(int id)
    {
        var member = new Member();
        member = await GetOneMemberAsync(id);

        member.TotalReply++;

        await _dbContext.SaveChangesAsync();
    }    
}

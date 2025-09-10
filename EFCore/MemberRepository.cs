using Entity;
using Microsoft.EntityFrameworkCore;


namespace EFCore;
public class MemberRepository : IMemberRepository
{
    private readonly MyDbContext _dbContext;

    public MemberRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<Member>> AllMembersAsync() => await _dbContext.Member.OrderBy(m => m.UserName).ToListAsync();
    public async Task<Member> GetMemberAsync(int id) => await _dbContext.Member.Where(m => m.Id == id).SingleOrDefaultAsync();
    public async Task UpdateMemberAsync(Member member)
    {
        _dbContext.Member.Update(member);
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
}

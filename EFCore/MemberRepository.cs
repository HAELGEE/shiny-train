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
    public async Task<Member> GetMemberByEmailAsync(string email) => await _dbContext.Member.Where(m => m.Email == email).SingleOrDefaultAsync();
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
        // Skall användas vid Display av Ålder
        //string date = DateTime.Now.ToShortDateString();
        //string dateToNumber = date.Replace("-", "");
        //int todayDate = int.Parse(dateToNumber);

        //member.Age = todayDate - member.Age;


        _dbContext.Add(member);
        await _dbContext.SaveChangesAsync();
    }
}

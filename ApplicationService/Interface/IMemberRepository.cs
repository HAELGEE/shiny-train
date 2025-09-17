using Entity;
using Microsoft.EntityFrameworkCore;

namespace EFCore;
public interface IMemberRepository
{
    Task CreateMemberAsync(Member member);
    Task DeleteMemberAsync(Member member);
    Task<List<Member>> GetAllMembersAsync();
    Task<Member> GetOneMemberAsync(int id);
    Task<Member> GetMemberByUsernameAsync(string userName);
    Task<List<Member>> GetAdminMembersAsync();
    Task<Member> GetAdminMemberAsync(int id);
    Task<Member> GetMemberByEmailAsync(string email);
    Task<Member> GetMemberByUsernamePasswordAsync(string username, string password);
    Task UpdateMemberAsync(Member member);
    Task UpdateMemberAdminrightsAsync(int id, bool isAdmin);
    Task UpdateProfileViewsAsync(int id);
    Task UpdateReportsForMemberAsync(int id);
    Task UpdateProfilePostCounterAsync(int id);
    Task UpdateProfileReplyCounterAsync(int id);

    
}
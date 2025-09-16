using EFCore;
using Entity;

namespace ApplicationService.Interface;
public interface IMemberService
{
    Task CreateMemberAsync(Member member);
    Task DeleteMemberAsync(Member member);
    Task<List<Member>> GetAllMembersAsync();
    Task<Member> GetOneMemberAsync(int id);
    Task<Member> GetMemberByUsernameAsync(string userName);
    Task<Member> GetMemberByEmailAsync(string email);
    Task<Member> GetMemberByUsernamePasswordAsync(string username, string password);
    Task UpdateMemberAsync(Member member);
    Task UpdateProfileViewsAsync(int id);
    Task UpdateProfilePostCounterAsync(int id);
    Task UpdateProfileReplyCounterAsync(int id);
}
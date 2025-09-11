using EFCore;
using Entity;

namespace ApplicationService.Interface;
public interface IMemberService
{
    Task CreateMemberAsync(Member member);
    Task DeleteMemberAsync(Member member);
    Task<List<Member>> GetAllMembersAsync();
    Task<Member> GetOneMemberAsync(int id);
    Task<Member> GetMemberByEmailAsync(string email);
    Task UpdateMemberAsync(Member member);
}
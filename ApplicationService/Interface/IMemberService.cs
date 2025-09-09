using Entity;

namespace ApplicationService.Interface;
public interface IMemberService
{
    Task CreateMember(Member member);
    Task DeleteMemberAsync(Member member);
    Task<List<Member>> GetAllMembersAsync();
    Task<Member> GetOneMemberAsync(int id);
    Task UpdateMemberAsync(Member member);
}
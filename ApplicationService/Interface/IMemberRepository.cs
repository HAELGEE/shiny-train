using Entity;

namespace EFCore;
public interface IMemberRepository
{
    Task<List<Member>> AllMembersAsync();
    Task CreateMemberAsync(Member member);
    Task DeleteMemberAsync(Member member);
    Task<Member> GetMemberAsync(int id);
    Task UpdateMemberAsync(Member member);
}
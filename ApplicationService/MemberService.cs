using ApplicationService.Interface;
using EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace ApplicationService;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<List<Member>> GetAllMembersAsync()
    {
        return await _memberRepository.AllMembersAsync();
    }
    public async Task<Member> GetOneMemberAsync(int id)
    {
        return await _memberRepository.GetMemberAsync(id);
    }

    public async Task UpdateMemberAsync(Member member)
    {
        await _memberRepository.UpdateMemberAsync(member);
    }
    public async Task DeleteMemberAsync(Member member)
    {

        await _memberRepository.DeleteMemberAsync(member);
    }

    public async Task CreateMember(Member member)
    {

        await _memberRepository.CreateMemberAsync(member);
    }

}

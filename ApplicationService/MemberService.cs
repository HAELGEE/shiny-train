using ApplicationService.Interface;
using EFCore;
using Entity;
using static QRCoder.PayloadGenerator;

namespace ApplicationService;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task CreateMemberAsync(Member member)
    {
        await _memberRepository.CreateMemberAsync(member);
    }
    public async Task DeleteMemberAsync(Member member)
    {
        await _memberRepository.DeleteMemberAsync(member);
    }
    public async Task<List<Member>> GetAllMembersAsync()
    {
        return await _memberRepository.GetAllMembersAsync();
    }
    public async Task<Member> GetOneMemberAsync(int id)
    {
        return await _memberRepository.GetOneMemberAsync(id);
    }
    public async Task<Member> GetMemberByUsernameAsync(string userName)
    {
        return await _memberRepository.GetMemberByUsernameAsync(userName);
    }
    public async Task<Member> GetMemberByEmailAsync(string email)
    {
        return await _memberRepository.GetMemberByEmailAsync(email);
    }
   public async Task<Member> GetMemberByUsernamePasswordAsync(string username, string password)
    {
        return await _memberRepository.GetMemberByUsernamePasswordAsync(username, password);
    }

    public async Task UpdateMemberAsync(Member member)
    {
        await _memberRepository.UpdateMemberAsync(member);
    }
    public async Task UpdateProfileViewsAsync(int id)
    {
        await _memberRepository.UpdateProfileViewsAsync(id);
    }
    public async Task UpdateProfilePostCounterAsync(int id)
    {
        await _memberRepository.UpdateProfilePostCounterAsync(id);
    }
    public async Task UpdateProfileReplyCounterAsync(int id)
    {
        await _memberRepository.UpdateProfileReplyCounterAsync(id);
    }


}

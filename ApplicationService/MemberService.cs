using ApplicationService.Interface;
using EFCore;
using Entity;

namespace ApplicationService;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;
    private readonly IReportsRepository _reportsRepository;

    public MemberService(IMemberRepository memberRepository, IReportsRepository reportsRepository)
    {
        _memberRepository = memberRepository;
        _reportsRepository = reportsRepository;
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
    public async Task<List<Member>> GetAdminMembersAsync()
    {
        return await _memberRepository.GetAdminMembersAsync();
    }
    public async Task<Member> GetAdminMemberAsync(int id)
    {
        return await _memberRepository.GetAdminMemberAsync(id);
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
    public async Task UpdateMemberAdminrightsAsync(int id, bool isAdmin)
    {
        await _memberRepository.UpdateMemberAdminrightsAsync(id, isAdmin);
    }    
    public async Task UpdateReportsForMemberAsync(int id)
    {
       await _memberRepository.UpdateReportsForMemberAsync(id);
    }
    public async Task UpdateProfilePostCounterAsync(int id)
    {
        await _memberRepository.UpdateProfilePostCounterAsync(id);  
    }
    
    public async Task UpdateProfileReplyCounterAsync(int id)
    {
        await _memberRepository.UpdateProfileReplyCounterAsync(id);
    }
    public async Task AddProfileViewAsync(int userId, int visitorId)
    {
        await _memberRepository.AddProfileViewAsync(userId, visitorId);
    }
    public async Task ViewProfileViewsAsync(int id)
    {
        await _memberRepository.ViewProfileViewsAsync(id);
    }

    // Reports
    public async Task<List<Reports>> GettingAllReportsAsync()
    {
        return await _reportsRepository.GettingAllReportsAsync();
    }
    public async Task DeleteReportsAsync(Reports reports)
    {
        await _reportsRepository.DeleteReportsAsync(reports);
    }
}

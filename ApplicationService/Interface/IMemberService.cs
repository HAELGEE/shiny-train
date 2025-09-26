using Entity;

namespace ApplicationService.Interface;
public interface IMemberService
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
    Task UpdateReportsForMemberAsync(int id);    
    Task UpdateProfilePostCounterAsync(int id);
    Task UpdateProfileSubReplyCounterAsync(int id);         
    Task AddProfileViewAsync(int userId, int visitorId);
    Task ViewProfileViewsAsync(int id);    
    Task CreateChattWithUserAsync(Chatt chatt);
    Task DeleteChattWithUserAsync(int userId, int receiverId);
    Task<List<Chatt>> GetAllChattMessagesFromReceiverIdAsync(int userId, int receiverId);
    Task<List<Chatt>> AllChatForMemberAsync(int userId);

    // Reports
    Task<List<Reports>> GettingAllReportsAsync();
    Task DeleteReportsAsync(Reports reports);
}
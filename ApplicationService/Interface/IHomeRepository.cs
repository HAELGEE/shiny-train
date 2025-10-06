using Entity;

namespace EFCore;
public interface IHomeRepository
{
    Task<List<Member>> GetMemberByUsernameAsync(string username);
    Task<List<Post>> GetPostByTitleAsync(string title);
    Task<List<Post>> GetSubpostAndPostByTextAsync(string text);
}
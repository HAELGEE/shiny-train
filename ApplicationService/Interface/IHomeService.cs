using Entity;

namespace ApplicationService.Interface;
public interface IHomeService
{
    Task<List<Member>> GetMemberByUsernameAsync(string username);
    Task<List<Post>> GetPostByTitleAsync(string title);
    Task<List<Post>> GetSubpostAndPostByTextAsync(string text);
}
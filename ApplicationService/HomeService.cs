using ApplicationService.Interface;
using EFCore;
using Entity;


namespace ApplicationService;
public class HomeService : IHomeService
{
    private readonly IHomeRepository _homeRepository;

    public HomeService(IHomeRepository homeRepository)
    {
        _homeRepository = homeRepository;
    }

    public async Task<List<Member>> GetMemberByUsernameAsync(string username)
    {
        return await _homeRepository.GetMemberByUsernameAsync(username);
    }

    public async Task<List<Post>> GetPostByTitleAsync(string title)
    {
        return await _homeRepository.GetPostByTitleAsync(title);
    }

    public async Task<List<Post>> GetSubpostAndPostByTextAsync(string text)
    {
        return await _homeRepository.GetSubpostAndPostByTextAsync(text);
    }
}

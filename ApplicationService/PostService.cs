using ApplicationService.Interface;
using EFCore;
using Entity;

namespace ApplicationService;
public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<List<Post>> GettingAllPostForSubCategoryAsync(int categoryId)
    {
        return await _postRepository.GettingAllPostForSubCategoryAsync(categoryId);
    }

    public async Task<List<Post>> GettingAllRecentPostAsync()
    {
        return await _postRepository.GettingAllRecentPostAsync();
    }
}

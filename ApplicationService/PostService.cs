using ApplicationService.Interface;
using EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace ApplicationService;
public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    // Post
    public async Task<List<Post>> GetAllReportsAsync()
    {
        return await _postRepository.GetAllReportsAsync();
    }
    public async Task<Post> GetOnePostAsync(int id)
    {
        return await _postRepository.GetOnePostAsync(id);
    }

    public async Task<List<Post>> GettingAll100RecentPosts(int memberId)
    {
        return await _postRepository.GettingAll25RecentPostsAsync(memberId);
    }
    public async Task<List<Post>> GettingAllPostForSubCategoryAsync(int categoryId)
    {
        return await _postRepository.GettingAllPostForSubCategoryAsync(categoryId);
    }
    public async Task<List<Post>> Getting10RecentPostByReplyAsync()
    {
        return await _postRepository.Getting10RecentPostByReplyAsync();
    }
    public async Task CreatePostAsync(Post post)
    {
       await _postRepository.CreatePostAsync(post);
    }
    public async Task DeletePostAsync(Post post)
    {
        await _postRepository.DeletePostAsync(post);
    }

    // Subpost
    public async Task<List<SubPost>> GettingSubPostFromPostById(int id)
    {
        return await _postRepository.GettingSubPostFromPostByIdAsync(id);
    }
    public async Task CreateSubPostAsync(SubPost subPost)
    {
        await _postRepository.CreateSubPostAsync(subPost);
    }
    public async Task DeleteSubPostAsync(int id)
    {
        await _postRepository.DeleteSubPostAsync(id);
    }
    public async Task<SubPost> GetOneSubPostAsync(int id)
    {
        return await _postRepository.GetOneSubPostAsync(id);
    }
}

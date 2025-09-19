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

    public async Task<List<Post>> GettingAll25RecentPostsAsync()
    {
        return await _postRepository.GettingAll25RecentPostsAsync();
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
    public async Task UpdatePostAsync(Post post)
    {
        await _postRepository.UpdatePostAsync(post);
    }
    public async Task ReportPostAsync(int id, int reporterId)
    {
        await _postRepository.ReportPostAsync(id, reporterId);
    }
    public async Task UpdatePostReplyCounterAsync(int id)
    {
        await _postRepository.UpdatePostReplyCounterAsync(id);
    }    
    public async Task UpdatePostViewsCounterAsync(int postId, int memberId)
    {
        await _postRepository.UpdatePostViewsCounterAsync(postId, memberId);
    }
   


    // Subpost
    public async Task<List<SubPost>> GettingSubPostFromPostByIdAsync(int id)
    {
        return await _postRepository.GettingSubPostFromPostByIdAsync(id);
    }
    public async Task CreateSubPostAsync(SubPost subPost)
    {
        await _postRepository.CreateSubPostAsync(subPost);
    }
    public async Task DeleteSubPostAsync(SubPost subPost)
    {
        await _postRepository.DeleteSubPostAsync(subPost);
    }
    public async Task<SubPost> GetOneSubPostAsync(int id)
    {
        return await _postRepository.GetOneSubPostAsync(id);
    }
    public async Task UpdateSubPostAsync(SubPost subPost)
    {
        await _postRepository.UpdateSubPostAsync(subPost);
    }

    
}

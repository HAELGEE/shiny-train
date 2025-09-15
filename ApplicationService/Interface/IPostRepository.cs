using Entity;
using Microsoft.EntityFrameworkCore;

namespace EFCore;
public interface IPostRepository
{
    // Post
    Task<Post> GetOnePostAsync(int id);
    Task<List<Post>> GettingAll25RecentPostsAsync(int memberId);
    Task<List<Post>> GettingAllPostForSubCategoryAsync(int categoryId);
    Task<List<Post>> Getting10RecentPostByReplyAsync();
    Task CreatePostAsync(Post post);
    Task DeletePostAsync(Post post);

    // SubPost
    Task<List<SubPost>> GettingSubPostFromPostByIdAsync(int id);
    Task CreateSubPostAsync(SubPost subPost);
    Task DeleteSubPostAsync(SubPost subPost);
    
}
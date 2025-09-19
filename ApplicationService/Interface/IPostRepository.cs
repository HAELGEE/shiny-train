using Entity;
using Microsoft.EntityFrameworkCore;

namespace EFCore;
public interface IPostRepository
{
    // Post
    Task<List<Post>> GetAllReportsAsync();
    Task<Post> GetOnePostAsync(int id);
    Task<List<Post>> GettingAll25RecentPostsAsync();
    Task<List<Post>> GettingAllPostForSubCategoryAsync(int categoryId);
    Task<List<Post>> Getting10RecentPostByReplyAsync();
    Task CreatePostAsync(Post post);
    Task DeletePostAsync(Post post);
    Task UpdatePostAsync(Post post);
    Task ReportPostAsync(int id, int reporterId);


    // SubPost
    Task<List<SubPost>> GettingSubPostFromPostByIdAsync(int id);
    Task CreateSubPostAsync(SubPost subPost);
    Task DeleteSubPostAsync(SubPost subPost);
    Task<SubPost> GetOneSubPostAsync(int id);


}
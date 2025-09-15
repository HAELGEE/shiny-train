using Entity;

namespace ApplicationService.Interface;

public interface IPostService
{
    // Post
    Task<List<Post>> GetAllReportsAsync();
    Task<Post> GetOnePostAsync(int id);
    Task<List<Post>> GettingAll100RecentPosts(int memberId);
    Task<List<Post>> GettingAllPostForSubCategoryAsync(int categoryId);
    Task<List<Post>> Getting10RecentPostByReplyAsync();
    Task CreatePostAsync(Post post);
    Task DeletePostAsync(Post post);

    // SubPost
    Task<List<SubPost>> GettingSubPostFromPostById(int id);
    Task CreateSubPostAsync(SubPost subPost);
    Task DeleteSubPostAsync(int id);
    Task<SubPost> GetOneSubPostAsync(int id);
}
using Entity;

namespace ApplicationService.Interface;

public interface IPostService
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
    Task UnReportPostAsync(int postId, int reporterId);
    Task UpdatePostReplyCounterAsync(int id);
    Task UpdatePostViewsCounterAsync(int postId, int memberId);
    Task UpdatePostLikesCounterAsync(int postId, int memberId);


    // SubPost
    Task<List<SubPost>> GettingSubPostFromPostByIdAsync(int id);
    Task CreateSubPostAsync(SubPost subPost);
    Task DeleteSubPostAsync(SubPost subPost);
    Task<SubPost> GetOneSubPostAsync(int id);
    Task UpdateSubPostAsync(SubPost subPost);
}
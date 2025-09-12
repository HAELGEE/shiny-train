using Entity;

namespace ApplicationService.Interface;

public interface IPostService
{
    Task<List<Post>> GettingAllPostForSubCategoryAsync(int categoryId);
    Task<List<Post>> GettingAllRecentPostAsync();
}
using Entity;

namespace EFCore;
public interface IPostRepository
{
    Task<List<Post>> GettingAllPostForSubCategoryAsync(int categoryId);
    Task<List<Post>> GettingAllRecentPostAsync();
}
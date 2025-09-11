using Entity;

namespace EFCore;
public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task CreateCategoryAsync(Category category);
}
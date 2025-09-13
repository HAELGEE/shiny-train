using Entity;

namespace ApplicationService.Interface;
public interface ICategoryService
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task CreateCategoryAsync(Category category);
    Task<Category> GetOneCategoriesAsync(int id);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
}
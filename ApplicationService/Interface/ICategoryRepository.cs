using Entity;

namespace EFCore;
public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task<List<SubCategory>> GetAllSubCategoriesAsync();
    Task CreateCategoryAsync(Category category);
    Task<Category> GetOneCategoriesAsync(int id);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
}
using Entity;

namespace ApplicationService.Interface;
public interface ICategoryService
{
    // Category
    Task<List<Category>> GetAllCategoriesAsync();
    Task CreateCategoryAsync(Category category);
    Task<Category> GetOneCategoriesAsync(int id);
    Task<Category> GetOneByNameCategoriesAsync(string name);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);

    // Subcategory
    Task<List<SubCategory>> GetAllSubCategoriesAsync();
    Task<SubCategory> GetOneSubCategoriesAsync(int id);
    Task<SubCategory> GetOneByNameSubCategoriesAsync(string name);
    Task CreateSubCategoryAsync(SubCategory SubCategory);
    Task UpdateSubCategoryAsync(SubCategory SubCategory);
    Task DeleteSubCategoryAsync(SubCategory SubCategory);
}
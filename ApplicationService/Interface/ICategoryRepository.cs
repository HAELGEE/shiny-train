using Entity;
using Microsoft.EntityFrameworkCore;

namespace EFCore;
public interface ICategoryRepository
{
    // Category
    Task<List<Category>> GetAllCategoriesAsync();
    Task CreateCategoryAsync(Category category);
    Task<Category> GetOneCategoriesAsync(int id);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);

    // Subcategory
    Task<List<SubCategory>> GetAllSubCategoriesAsync();
    Task<SubCategory> GetOneSubCategoriesAsync(int id);
    Task CreateSubCategoryAsync(SubCategory SubCategory);
    Task UpdateSubCategoryAsync(SubCategory SubCategory);
    Task DeleteSubCategoryAsync(SubCategory SubCategory);
}
using Entity;

namespace ApplicationService.Interface;
public interface ICategoryService
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task CreateCategoryAsync(Category category);

}
using ApplicationService.Interface;
using EFCore;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService;
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _categoryRepository.GetAllCategoriesAsync();
    }
    public async Task CreateCategoryAsync(Category category)
    {
        await _categoryRepository.CreateCategoryAsync(category);        
    }
    public async Task<Category> GetOneCategoriesAsync(int id)
    {
        return await _categoryRepository.GetOneCategoriesAsync(id);
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        await _categoryRepository.UpdateCategoryAsync(category);
    }
    public async Task DeleteCategoryAsync(Category category)
    {
        await _categoryRepository.DeleteCategoryAsync(category);
    }

}

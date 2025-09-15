using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore;
public class CategoryRepository : ICategoryRepository
{
    private readonly MyDbContext _context;

    public CategoryRepository(MyDbContext context)
    {
        _context = context;
    }


    // Category
    public async Task<List<Category>> GetAllCategoriesAsync() => await _context.Category.ToListAsync();
    public async Task<Category> GetOneCategoriesAsync(int id) => await _context.Category.Where(x => x.Id == id).SingleOrDefaultAsync();
    public async Task<Category> GetOneByNameCategoriesAsync(string name) => await _context.Category.Where(x => x.Name == name).SingleOrDefaultAsync();



    public async Task CreateCategoryAsync(Category category)
    {
        _context.Category.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        _context.Category.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        _context.Category.Remove(category);
        await _context.SaveChangesAsync();
    }

    // Subcategory
    public async Task<List<SubCategory>> GetAllSubCategoriesAsync() => await _context.SubCategory.ToListAsync();
    public async Task<SubCategory> GetOneSubCategoriesAsync(int id) => await _context.SubCategory.Where(x => x.Id == id).SingleOrDefaultAsync(); 
    public async Task<SubCategory> GetOneByNameSubCategoriesAsync(string name) => await _context.SubCategory.Where(x => x.Name == name).SingleOrDefaultAsync(); 

    public async Task CreateSubCategoryAsync(SubCategory SubCategory)
    {
        _context.SubCategory.Add(SubCategory);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateSubCategoryAsync(SubCategory SubCategory)
    {
        _context.SubCategory.Update(SubCategory);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSubCategoryAsync(SubCategory SubCategory)
    {
        _context.SubCategory.Remove(SubCategory);
        await _context.SaveChangesAsync();
    }
}

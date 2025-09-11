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

    public async Task<List<Category>> GetAllCategoriesAsync() => await _context.Category.ToListAsync();

    public async Task CreateCategoryAsync(Category category)
    {
        _context.Category.Add(category);
        await _context.SaveChangesAsync();
    }
}

using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore;
public class PostRepository : IPostRepository
{
    private readonly MyDbContext _context;

    public PostRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<Post>> GettingAllPostForSubCategoryAsync(int categoryId) =>
        await _context.Post
            .Where(x => x.SubCategoryId == categoryId)
            .Include(p => p.SubPosts)            
            .OrderByDescending(x => x.Created)
            .ToListAsync();

    public async Task<List<Post>> GettingAllRecentPostAsync() =>
        await _context.Post            
            .Include(p => p.SubPosts)
            .Take(10)
            .OrderByDescending(x => x.Created)
            .ToListAsync();
}

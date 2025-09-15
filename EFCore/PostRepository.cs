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

    // Post
    public async Task<List<Post>> GetAllReportsAsync() => await _context.Post
        .Where(p => p.Reported == true)
        .Include(p => p.Member)
        .ToListAsync();

    public async Task<Post> GetOnePostAsync(int id) => await _context.Post
        .Where(p => p.Id == id)
        .Include(p => p.Member)
        .Include(p => p.SubPosts)
        .SingleOrDefaultAsync();
    
    public async Task<List<Post>> GettingAll25RecentPostsAsync(int memberId) => 
        await _context.Post
        .Where(p => p.MemberId == memberId)  
        .Include(p => p.Member)
        .OrderByDescending(p => p.Created)
        .Take(25)
        .ToListAsync();

    public async Task<List<Post>> GettingAllPostForSubCategoryAsync(int categoryId) =>
        await _context.Post
            .Where(x => x.SubCategoryId == categoryId)
            .Include(p => p.SubPosts)            
            .OrderByDescending(x => x.Created)
            .ToListAsync();

    public async Task<List<Post>> Getting10RecentPostByReplyAsync() =>
        await _context.Post            
            .Include(p => p.SubPosts)
            .Take(10)
            .OrderByDescending(x => x.Reply)
            .ToListAsync();

    public async Task CreatePostAsync(Post post)
    {
        _context.Post.Add(post);
        await _context.SaveChangesAsync();
    }
    public async Task DeletePostAsync(Post post)
    {        
        _context.Post.Remove(post);
        await _context.SaveChangesAsync();
    }

    // Subpost

    public async Task<SubPost> GetOneSubPostAsync(int id) => await _context.SubPost.Where(sp => sp.Id == id).SingleOrDefaultAsync();
    public async Task<List<SubPost>> GettingSubPostFromPostByIdAsync(int id) => await _context.SubPost
        .Where(sp => sp.PostId == id)
        .Include(sp => sp.Member)
        .ToListAsync();

    public async Task CreateSubPostAsync(SubPost subPost)
    {
        _context.SubPost.Add(subPost);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteSubPostAsync(int id)
    {
        var subPost = await GetOneSubPostAsync(id);

        _context.SubPost.Remove(subPost);
        await _context.SaveChangesAsync();
    }

}

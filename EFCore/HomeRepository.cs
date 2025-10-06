using Entity;
using Microsoft.EntityFrameworkCore;


namespace EFCore;
public class HomeRepository : IHomeRepository
{
    private readonly MyDbContext _context;

    public HomeRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<Member>> GetMemberByUsernameAsync(string username) => await _context.Member.Where(m => m.UserName.Contains(username))
        .OrderBy(m => m.UserName)
        .ToListAsync();

    public async Task<List<Post>> GetPostByTitleAsync(string title) => await _context.Post.Where(p => p.Description.Contains(title))
        .OrderByDescending(p => p.Created)
        .ToListAsync();

    public async Task<List<Post>> GetSubpostAndPostByTextAsync(string text) => await _context.Post.Where(p => p.Text.Contains(text))
        .Include(p => p.SubPosts.Where(sp => sp.Text == text).OrderBy(p => p.Text)).ToListAsync();
}

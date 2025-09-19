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
        .Include(p => p.ReporterIds)
        .ToListAsync();

    public async Task<Post> GetOnePostAsync(int id) => await _context.Post
        .Where(p => p.Id == id)
        .Include(p => p.Member)
        .Include(p => p.SubPosts)
        .Include(p => p.ReporterIds)
        .SingleOrDefaultAsync();

    public async Task<List<Post>> GettingAll25RecentPostsAsync() =>
        await _context.Post
        .Take(25)
        .OrderByDescending(p => p.Created)
        .Include(p => p.Member)
        .ToListAsync();

    public async Task<List<Post>> GettingAllPostForSubCategoryAsync(int categoryId) =>
        await _context.Post
            .Where(x => x.SubCategoryId == categoryId)
            .Include(p => p.SubPosts)
        .Include(p => p.Member)
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
    public async Task UpdatePostAsync(Post post)
    {
        _context.Update(post);
        await _context.SaveChangesAsync();
    }
    public async Task ReportPostAsync(int postId, int reporterId)
    {
        var post = await GetOnePostAsync(postId);
        var member = await _context.Member.Where(m => m.Id == post.MemberId).FirstOrDefaultAsync();
        bool check = false;

        foreach (var report in post.ReporterIds)
        {
            if (report.MemberId == reporterId)
            {
                check = true; break;
            }
        }

        if (!check)
        {
            post.Reported = true;

            var report = new Reports
            {
                MemberId = reporterId,
                PostId = postId,
            };

            _context.Reports.Add(report);
            _context.Update(post);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdatePostReplyCounterAsync(int id)
    {
        var post = await GetOnePostAsync(id);

        post.Reply++;

        _context.Update(post);
        await _context.SaveChangesAsync();
    }


    public async Task UpdatePostViewsCounterAsync(int postId, int memberId)
    {
        var viewCheck = await _context.Views.Where(vm => vm.MemberId == memberId).FirstOrDefaultAsync();

        var post = await GetOnePostAsync(postId);

        if (viewCheck == null || post.Id != viewCheck.PostId)
        {
            var member = await _context.Member.Where(m => m.Id == memberId).FirstOrDefaultAsync();


            var view = new View
            {
                PostId = post.Id,
                MemberId = member.Id,
            };

            _context.Add(view);
            await _context.SaveChangesAsync();
        }
    }

    // Subpost

    public async Task<SubPost> GetOneSubPostAsync(int id) => await _context.SubPost.Where(sp => sp.Id == id).FirstOrDefaultAsync();
    public async Task<List<SubPost>> GettingSubPostFromPostByIdAsync(int id) => await _context.SubPost
        .Where(sp => sp.PostId == id)
        .Include(sp => sp.Member)
        .ToListAsync();

    public async Task CreateSubPostAsync(SubPost subPost)
    {
        _context.SubPost.Add(subPost);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteSubPostAsync(SubPost subPost)
    {
        _context.SubPost.Remove(subPost);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateSubPostAsync(SubPost subPost)
    {
        _context.Update(subPost);
        await _context.SaveChangesAsync();
    }

}

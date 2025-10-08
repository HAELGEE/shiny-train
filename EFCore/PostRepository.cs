using Entity;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

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

    public async Task<List<Post>> GetAllPosts() => await _context.Post.ToListAsync();

    public async Task<List<SubPost>> GetAllSubpostReportsAsync() => await _context.SubPost
        .Where(p => p.Reported == true)
        .Include(p => p.Member)
        .Include(p => p.ReporterIds)
        .ToListAsync();

    public async Task<Post> GetOnePostAsync(int id) => await _context.Post
        .Where(p => p.Id == id)
        .Include(p => p.Member)
        .Include(p => p.SubPosts)
        .Include(p => p.ReporterIds)
        .Include(p => p.Likes)
        .Include(p => p.Views)
        .SingleOrDefaultAsync();

    public async Task<List<Post>> GettingAll25RecentPostsAsync() =>
        await _context.Post
        .OrderByDescending(p => p.Created)
        .Take(25)
        .Include(p => p.Member)
        .Include(p => p.Views)
        .Include(p => p.Likes)
        .ToListAsync();

    public async Task<List<Post>> GettingAllPostForSubCategoryAsync(int categoryId) =>
        await _context.Post
            .Where(x => x.SubCategoryId == categoryId)
            .Include(p => p.SubPosts)
            .Include(p => p.Member)
            .Include(p => p.Likes)
            .Include(p => p.Views)
            .OrderByDescending(x => x.Created)
            .ToListAsync();

    public async Task<List<Post>> Getting10RecentPostByReplyAsync() =>
        await _context.Post
            .Include(p => p.SubPosts)
            .OrderByDescending(x => x.Reply)
            .Take(10)
            .ToListAsync();

    public async Task CreatePostAsync(Post post)
    {
        _context.Post.Add(post);
        await _context.SaveChangesAsync();
    }
    public async Task DeletePostAsync(Post post)
    {
        if (post.ImagePath != null && post.ImagePath != "/uploads/standardProfile.png")
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", post.ImagePath.TrimStart('/'));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        if(post.Views.Count > 0)
        {
            foreach (var view in post.Views)
            {
                _context.Remove(view);
            }
            await _context.SaveChangesAsync();
        }

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
    public async Task UnReportPostAsync(int postId, int reporterId)
    {
        var post = await GetOnePostAsync(postId);

        var unreportMembers = await _context.Reports.Where(r => r.PostId == postId).ToListAsync();

        if (unreportMembers != null)
        {
            foreach (var report in unreportMembers)
            {
                _context.Reports.Remove(report);
            }

            post.Reported = false;
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
        var viewCheck = await _context.PostViews.Where(vm => vm.MemberId == memberId && vm.PostId == postId).FirstOrDefaultAsync();

        var post = await GetOnePostAsync(postId);
        var member = await _context.Member.Where(m => m.Id == memberId).FirstOrDefaultAsync();

        if (viewCheck == null && post.MemberId != memberId && memberId > 0)
        {
            var view = new PostView
            {
                PostId = post.Id,
                MemberId = member.Id,
            };

            _context.Add(view);

            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdatePostLikesCounterAsync(int postId, int memberId)
    {
        var likeCheck = await _context.Likes.Where(vm => vm.MemberId == memberId && vm.PostId == postId).FirstOrDefaultAsync();

        var post = await GetOnePostAsync(postId);
        var member = await _context.Member.Where(m => m.Id == memberId).FirstOrDefaultAsync();

        if (likeCheck == null && post.MemberId != memberId)
        {
            var like = new Likes
            {
                PostId = post.Id,
                MemberId = member.Id,
            };

            _context.Add(like);

            await _context.SaveChangesAsync();
        }
        else if (likeCheck != null && post.MemberId != memberId)
        {
            _context.Remove(likeCheck);

            await _context.SaveChangesAsync();
        }
    }



    // Subpost

    public async Task<SubPost> GetOneSubPostAsync(int id) => await _context.SubPost.Where(sp => sp.Id == id).Include(sb => sb.ReporterIds).FirstOrDefaultAsync();
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
        if (subPost.ImagePath != null && subPost.ImagePath != "/uploads/standardProfile.png")
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", subPost.ImagePath.TrimStart('/'));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        var post = await _context.Post.Where(p => p.Id == subPost.PostId).FirstOrDefaultAsync();
        post.Reply--;
        await _context.SaveChangesAsync();

        _context.SubPost.Remove(subPost);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateSubPostAsync(SubPost subPost)
    {
        _context.Update(subPost);
        await _context.SaveChangesAsync();
    }

    public async Task<List<SubPost>> GetAllChildSubpostsAsync(int id) => await _context.SubPost.Where(sb => sb.ParentSubpostId == id).ToListAsync();

    public async Task ReportSubpostAsync(int SubpostId, int reporterId)
    {
        var subpost = await GetOneSubPostAsync(SubpostId);

        var member = await _context.Member.Where(m => m.Id == subpost.MemberId).FirstOrDefaultAsync();
        bool check = false;

        foreach (var report in subpost.ReporterIds)
        {
            if (report.MemberId == reporterId)
            {
                check = true; break;
            }
        }

        if (!check)
        {
            subpost.Reported = true;

            var report = new Reports
            {
                MemberId = reporterId,
                SubPostId = subpost.Id,
            };

            _context.Reports.Add(report);
            _context.Update(subpost);
            await _context.SaveChangesAsync();
        }
    }
    public async Task UnReportSubpostAsync(int SubpostId, int reporterId)
    {
        var subpost = await GetOneSubPostAsync(SubpostId);

        var unreportMembers = await _context.Reports.Where(r => r.SubPostId == SubpostId).ToListAsync();

        if (unreportMembers != null)
        {
            foreach (var report in unreportMembers)
            {
                _context.Reports.Remove(report);
            }

            subpost.Reported = false;
            _context.Update(subpost);
            await _context.SaveChangesAsync();
        }
    }

}

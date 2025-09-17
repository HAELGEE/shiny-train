using ApplicationService.Interface;
using EFCore;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Snackis.Controllers;

public class PostController : Controller
{
    private readonly IPostService _postService;
    private readonly ICategoryService _categoryService;
    private readonly IMemberService _memberService;

    public PostController(IPostService postService, ICategoryService categoryService, IMemberService memberService)
    {
        _postService = postService;
        _categoryService = categoryService;
        _memberService = memberService;
    }

    async Task<bool> CheckAdmin()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return true;

        var member = await _memberService.GetOneMemberAsync((int)userId);

        if (member == null)
            return true;


        if (!member.IsAdmin)
            return true;


        return false;
    }

    [HttpGet("ReadPost")]
    public async Task<IActionResult> ReadPost(int id)
    {
        var post = await _postService.GetOnePostAsync(id);
        var subPosts = await _postService.GettingSubPostFromPostByIdAsync(post.Id);

        var subCategory = await _categoryService.GetOneSubCategoriesAsync((int)post.SubCategoryId!);

        var view = new Views {
            Post = post,
            SubPosts = subPosts,
            SubCategory = subCategory,
        };

        return View(view);
    }
    [HttpGet("Report")]
    public async Task<IActionResult> Report(int id, int reporterId)
    {
        await _postService.ReportPostAsync(id, reporterId);

        return RedirectToAction(nameof(ReadPost), new { Id = id });
    }

    // Admin delete
    [HttpGet("DeletePost")]
    public async Task<IActionResult> DeletePost(int id)
    {
        bool isAdmin = await CheckAdmin();

        if (isAdmin)
            return RedirectToAction("Index", "Home");

        if (id != 0)
        {
            var post = await _postService.GetOnePostAsync(id);
            foreach(var report in post.ReporterIds)
            {
                await _memberService.DeleteReportsAsync(report);
            }

            await _postService.DeletePostAsync(post);

            return RedirectToAction("Admin", "Member");
        }

            return View();
    }

    // User delete
    [HttpGet("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var postCheck = await _postService.GetOnePostAsync(id);

        if(userId != postCheck.MemberId)
        {
            return RedirectToAction("Index", "Home");
        }

        if (id != 0)
        {
            var post = await _postService.GetOnePostAsync(id);

            foreach (var reports in post.ReporterIds!)
            {
                await _memberService.DeleteReportsAsync(reports);

            }
            await _postService.DeletePostAsync(post);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("CreatePost")]
    public async Task<IActionResult> CreatePost(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Login", "Member");
        }

        var post = new Post
        {
            SubCategoryId = id,
            MemberId = userId,
        };

        await _memberService.UpdateProfilePostCounterAsync((int)userId);
        return View(post);
    }

    [HttpPost("CreatePost")]
    public async Task<IActionResult> CreatePost(Post post)
    {
        if(!ModelState.IsValid)
            return View(post);

       await _postService.CreatePostAsync(post);

        return RedirectToAction("ReadPost", new { id = post.Id});
    }

    [HttpGet("UpdatePost")]
    public async Task<IActionResult> UpdatePost(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Login", "Member");
        }

        return View(await _postService.GetOnePostAsync(id));
    }

    [HttpPost("UpdatePost")]
    public async Task<IActionResult> UpdatePost(Post post)
    {
        await _postService.UpdatePostAsync(post);

        return RedirectToAction(nameof(ReadPost), new { Id = post.Id });
    }
}

using ApplicationService.Interface;
using Entity;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("LikeButton")]
    public async Task<IActionResult> LikeButton(int postId, int memberId)
    {
        if(memberId > 0)
        {            
            await _postService.UpdatePostLikesCounterAsync(postId, memberId);
        }

        return RedirectToAction(nameof(ReadPost), new { Id = postId });
    }

    [HttpGet("ReadPost")]
    public async Task<IActionResult> ReadPost(int id, int subPostId)
    {
        var post = await _postService.GetOnePostAsync(id);
        var subPosts = await _postService.GettingSubPostFromPostByIdAsync(post.Id);

        var subPost = new SubPost();

        if (subPostId > 0)
        {
            subPost = await _postService.GetOneSubPostAsync(subPostId);
        }

        var subCategory = await _categoryService.GetOneSubCategoriesAsync((int)post.SubCategoryId!);

        var view = new Entities
        {
            Post = post,
            SubPosts = subPosts,
            SubPost = subPost,
            SubCategory = subCategory,
        };



        if (HttpContext.Session.GetInt32("UserId") != null)
        {
            await _postService.UpdatePostViewsCounterAsync(id, (int)HttpContext.Session.GetInt32("UserId"));
        }


        return View(view);
    }

    [HttpGet("Report")]
    public async Task<IActionResult> Report(int id, int reporterId)
    {
        HttpContext.Session.SetInt32("updateSubpost", 0);
        await _postService.ReportPostAsync(id, reporterId);

        return RedirectToAction(nameof(ReadPost), new { Id = id });
    }
    [HttpGet("UnReport")]
    public async Task<IActionResult> UnReport(int id, int reporterId)
    {
        HttpContext.Session.SetInt32("updateSubpost", 0);
        await _postService.UnReportPostAsync(id, reporterId);

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
            foreach (var report in post.ReporterIds)
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
        var member = await _memberService.GetOneMemberAsync((int)userId);

        if (userId != postCheck.MemberId && member.IsAdmin == false)
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

    [HttpGet("DeleteSubPost")]
    public async Task<IActionResult> DeleteSubPost(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        var subPostCheck = await _postService.GetOneSubPostAsync(id);
        //var subPostCheck = await _postService.GetOnePostAsync(id);

        if (userId != subPostCheck.MemberId && HttpContext.Session.GetInt32("IsAdmin") == 0)
        {
            return RedirectToAction("Index", "Home");
        }

        if (id != 0)
        {
            var post = await _postService.GetOneSubPostAsync(id);

            //foreach (var reports in post.ReporterIds!)
            //{
            //    await _memberService.DeleteReportsAsync(reports);

            //}

            await _postService.DeleteSubPostAsync(post);
        }

        return RedirectToAction(nameof(ReadPost), new { Id = subPostCheck.PostId });
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
        if (!ModelState.IsValid)
            return View(post);

        await _postService.CreatePostAsync(post);

        return RedirectToAction("ReadPost", new { id = post.Id });
    }

    [HttpGet("UpdatePost")]
    public async Task<IActionResult> UpdatePost(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Login", "Member");
        }
        var post = await _postService.GetOnePostAsync(id);


        return View(post);
    }

    [HttpPost("UpdatePost")]
    public async Task<IActionResult> UpdatePost(Post post)
    {

        await _postService.UpdatePostAsync(post);

        return RedirectToAction(nameof(ReadPost), new { Id = post.Id });
    }

    [HttpPost("CreateSubPost")]
    public async Task<IActionResult> CreateSubPost(SubPost subPost)
    {
        if(!ModelState.IsValid)
            return RedirectToAction(nameof(ReadPost), new { Id = subPost.PostId });

        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Login", "Member");
        }

        if(subPost.Text == null)
            

        await _memberService.UpdateProfileReplyCounterAsync((int)userId);
        await _postService.UpdatePostReplyCounterAsync((int)subPost.PostId);
        await _postService.CreateSubPostAsync(subPost);

        return RedirectToAction(nameof(ReadPost), new { Id = subPost.PostId });
    }

    [HttpGet("UpdateSubPost")]
    public async Task<IActionResult> UpdateSubPost(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var subPost = await _postService.GetOneSubPostAsync(id);

        if (userId == null && subPost.MemberId != userId)
        {
            return RedirectToAction("Login", "Member");
        }

        HttpContext.Session.SetInt32("updateSubpost", id);

        return RedirectToAction(nameof(ReadPost), new { Id = subPost.PostId, subPostId = id });
    }

    [HttpPost("UpdateSubPost")]
    public async Task<IActionResult> UpdateSubPost(SubPost subPost)
    {
        HttpContext.Session.SetInt32("updateSubpost", 0);

        if (!ModelState.IsValid)
            return RedirectToAction(nameof(ReadPost), new { Id = subPost.PostId });  
        

        await _postService.UpdateSubPostAsync(subPost);

        return RedirectToAction(nameof(ReadPost), new { Id = subPost.PostId});
    }

   

}

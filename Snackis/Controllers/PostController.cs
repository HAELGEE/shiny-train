using EFCore;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Snackis.Controllers;

public class PostController : Controller
{
    private readonly IPostRepository _postRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMemberRepository _memberRepository;

    public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository, IMemberRepository memberRepository)
    {
        _postRepository = postRepository;
        _categoryRepository = categoryRepository;
        _memberRepository = memberRepository;
    }

    async Task<bool> CheckAdmin()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return true;

        var member = await _memberRepository.GetOneMemberAsync((int)userId);

        if (member == null)
            return true;


        if (!member.IsAdmin)
            return true;


        return false;
    }

    [HttpGet("ReadPost")]
    public async Task<IActionResult> ReadPost(int id)
    {
        var post = await _postRepository.GetOnePostAsync(id);
        var subPosts = await _postRepository.GettingSubPostFromPostByIdAsync(post.Id);

        var subCategory = await _categoryRepository.GetOneSubCategoriesAsync((int)post.SubCategoryId!);

        var view = new Views {
            Post = post,
            SubPosts = subPosts,
            SubCategory = subCategory,
        };

        return View(view);
    }
    [HttpGet("Report")]
    public async Task<IActionResult> Report(int id)
    {
        await _postRepository.ReportPostAsync(id);

        return RedirectToAction(nameof(ReadPost), new { Id = id });
    }

    // Admin delete
    [HttpGet("DeletePost")]
    public async Task<IActionResult> DeletePost(int id)
    {
        if (id != 0)
        {
            var post = await _postRepository.GetOnePostAsync(id);
            await _postRepository.DeletePostAsync(post);
            return RedirectToAction("Admin", "Member");
        }

            return View();
    }

    // User delete
    [HttpGet("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id != 0)
        {
            var post = await _postRepository.GetOnePostAsync(id);
            await _postRepository.DeletePostAsync(post);
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

        await _memberRepository.UpdateProfilePostCounterAsync((int)userId);
        return View(post);
    }

    [HttpPost("CreatePost")]
    public async Task<IActionResult> CreatePost(Post post)
    {
        if(!ModelState.IsValid)
            return View(post);

       await _postRepository.CreatePostAsync(post);

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

        return View(await _postRepository.GetOnePostAsync(id));
    }

    [HttpPost("UpdatePost")]
    public async Task<IActionResult> UpdatePost(Post post)
    {
        await _postRepository.UpdatePostAsync(post);

        return View();
    }
}

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

    [HttpGet("ReadPost")]
    public async Task<IActionResult> ReadPost(int id)
    {
        var post = await _postRepository.GetOnePostAsync(id);
        var subPosts = await _postRepository.GettingSubPostFromPostByIdAsync(post.Id);

        var view = new Views {
            Post = post,
            SubPosts = subPosts
        };

        return View(view);
    }
    
}

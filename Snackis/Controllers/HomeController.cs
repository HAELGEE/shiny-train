using EFCore;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Snackis.Models;
using System.Diagnostics;

namespace Snackis.Controllers;
public class HomeController : Controller
{
    private readonly IPostRepository _postRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMemberRepository _memberRepository;

    public HomeController(IPostRepository postRepository, ICategoryRepository categoryRepository, IMemberRepository memberRepository)
    {
        _postRepository = postRepository;
        _categoryRepository = categoryRepository;
        _memberRepository = memberRepository;
    }
    //public async Task<IActionResult> Index()
    public async Task<IActionResult> Index()
    {
        var recentPosts = await _postRepository.Getting10RecentPostByReplyAsync();
        //var allPosts = await _postRepository.GettingAllPostForSubCategoryAsync();
        var categories = await _categoryRepository.GetAllCategoriesAsync();
        var subCategories = await _categoryRepository.GetAllSubCategoriesAsync();


        var members = await _memberRepository.GetAllMembersAsync();

        var posts = new List<Post>();
        

        foreach (var member in members)
        {
            var addToPost = await _postRepository.GettingAll25RecentPostsAsync(member.Id);
            posts.AddRange(addToPost);
        }

        var fullModel = new FullViewModel
        {
            Posts = posts,
            Categorys = categories.ToList(),
            SubCategorys = subCategories.ToList()
        };

        return View(fullModel);
    }





    public IActionResult Info()
    {
        return View();
    }

























    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

using ApplicationService.Interface;
using EFCore;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Snackis.Models;
using System.Diagnostics;

namespace Snackis.Controllers;

public class HomeController : Controller
{
    private readonly IPostService _postService;
    private readonly ICategoryService _categoryService;
    private readonly IMemberService _memberService;

    public HomeController(IPostService postService, ICategoryService categoryService, IMemberService memberService)
    {
        _postService = postService;
        _categoryService = categoryService;
        _memberService = memberService;
    }
    //public async Task<IActionResult> Index()
    public async Task<IActionResult> Index()
    {
        var recentPosts = await _postService.Getting10RecentPostByReplyAsync();
        //var allPosts = await _postRepository.GettingAllPostForSubCategoryAsync();
        var categories = await _categoryService.GetAllCategoriesAsync();
        var subCategories = await _categoryService.GetAllSubCategoriesAsync();
        var top10 = await _postService.Getting10RecentPostByReplyAsync();

        var members = await _memberService.GetAllMembersAsync();

        var addToPost = await _postService.GettingAll25RecentPostsAsync();
                

        var fullModel = new FullViewModel
        {
            Posts = addToPost,
            Categorys = categories.ToList(),
            SubCategorys = subCategories.ToList(),
            Top10Posts = top10

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

using ApplicationService.Interface;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Snackis.Models;
using System.Diagnostics;
using System.Security.Permissions;
using static System.Net.Mime.MediaTypeNames;

namespace Snackis.Controllers;

public class HomeController : Controller
{
    private readonly IPostService _postService;
    private readonly ICategoryService _categoryService;
    private readonly IMemberService _memberService;
    private readonly IHomeService _homeService;

    public HomeController(IPostService postService, ICategoryService categoryService, IMemberService memberService, IHomeService homeService)
    {
        _postService = postService;
        _categoryService = categoryService;
        _memberService = memberService;
        _homeService = homeService;
    }


    //public async Task<IActionResult> Index(int search, string text)
    public async Task<IActionResult> Index(FullViewModel fullModel)
    {
        var addToPost = await _postService.GettingAll25RecentPostsAsync();
        var categories = await _categoryService.GetAllCategoriesAsync();
        var subCategories = await _categoryService.GetAllSubCategoriesAsync();
        var top10 = await _postService.Getting10RecentPostByReplyAsync();


        fullModel.Posts = addToPost;
        fullModel.Categorys = categories.ToList();
        fullModel.SubCategorys = subCategories.ToList();
        fullModel.Top10Posts = top10;

        if (fullModel.searchType == 1)
        {
            fullModel.Members = await _homeService.GetMemberByUsernameAsync(fullModel.Text);
        }
        else if (fullModel.searchType == 2)
        {
            fullModel.PostTitle = await _homeService.GetPostByTitleAsync(fullModel.Text);
        }
        else if (fullModel.searchType == 3)
        {
            fullModel.PostText = await _homeService.GetSubpostAndPostByTextAsync(fullModel.Text);
        }


        return View(fullModel);
    }

    [HttpGet("Search")]
    public async Task<IActionResult> Search(FullViewModel fullModel)
    {
        return RedirectToAction(nameof(Index), fullModel);
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

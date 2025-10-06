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


    public async Task<IActionResult> Index(int search, string text)
    {
        var addToPost = await _postService.GettingAll25RecentPostsAsync();
        var categories = await _categoryService.GetAllCategoriesAsync();
        var subCategories = await _categoryService.GetAllSubCategoriesAsync();
        var top10 = await _postService.Getting10RecentPostByReplyAsync();


        var fullModel = new FullViewModel
        {
            Posts = addToPost,
            Categorys = categories.ToList(),
            SubCategorys = subCategories.ToList(),
            Top10Posts = top10,
            searchType = search,
            Text = text
        };
        if (search == 1)
        {
            fullModel.Members = await _homeService.GetMemberByUsernameAsync(text);            
        }
        else if (search == 2)
        {
            fullModel.PostTitle = await _homeService.GetPostByTitleAsync(text);
        }
        else if (search == 3)
        {
            fullModel.PostText = await _homeService.GetSubpostAndPostByTextAsync(text);
        }


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

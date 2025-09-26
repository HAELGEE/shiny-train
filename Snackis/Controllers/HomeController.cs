using ApplicationService.Interface;
using Entity;
using Microsoft.AspNetCore.Mvc;
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

        if (members.Count > 0)
        {
            var admin = new Member
            {
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@admin.se",
                Birthday = "20000101",
                IsAdmin = true,
                IsOwner = true,
                Password = "password",
                UserName = "Admin",
            };
            await _memberService.CreateMemberAsync(admin);
        }


        if (categories.Count > 0)
        {
            var catogry1 = new Category
            {
                Name = "Cars",
            };
            var catogry2 = new Category
            {
                Name = "Hobbys",
            };
            var catogry3 = new Category
            {
                Name = "Gaming",
            };

            await _categoryService.CreateCategoryAsync(catogry1);
            await _categoryService.CreateCategoryAsync(catogry2);
            await _categoryService.CreateCategoryAsync(catogry3);
        }

        if (subCategories.Count > 0)
        {
            var subCategory1 = new SubCategory
            {
                CategoryId = 1,
                Name = "Volvo",
            };
            var subCategory2 = new SubCategory
            {
                CategoryId = 2,
                Name = "Fotball",
            };
            var subCategory3 = new SubCategory
            {
                CategoryId = 3,
                Name = "Dota2",
            };
            
            await _categoryService.CreateSubCategoryAsync(subCategory1);
            await _categoryService.CreateSubCategoryAsync(subCategory2);
            await _categoryService.CreateSubCategoryAsync(subCategory2);
        }



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

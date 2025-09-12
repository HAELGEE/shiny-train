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

    public HomeController(IPostRepository postRepository, ICategoryRepository categoryRepository)
    {
        _postRepository = postRepository;
        _categoryRepository = categoryRepository;
    }
    //public async Task<IActionResult> Index()
    public async Task<IActionResult> Index()
    {
        //var recentPosts = await _postRepository.GettingAllRecentPostAsync();
        //var allPosts = await _postRepository.GettingAllPostForSubCategoryAsync();
        var categories = await _categoryRepository.GetAllCategoriesAsync();

        var fullModel = new FullViewModel
        {
            //Posts = allPosts,            
            Categorys = categories.ToList()
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

using ApplicationService;
using ApplicationService.Interface;
using Entity;
using Microsoft.AspNetCore.Mvc;

namespace Snackis.Controllers;
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IMemberService _memberService;

    public CategoryController(ICategoryService categoryService, IMemberService memberService)
    {
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

    // Category CRUD
    [HttpGet("CreateCategory")]
    public async Task<IActionResult> CreateCategory()
    {
        
        if(await CheckAdmin())
            return RedirectToAction(nameof(Index), "Home");

        return View();
    }

    [HttpPost("CreateCategory")]
    public async Task<IActionResult> CreateCategory(Category category)
    {
        if (!ModelState.IsValid)
            return View();

        await _categoryService.CreateCategoryAsync(category);

        return RedirectToAction("Admin", "Member");
    }


    [HttpGet("UpdateCategory")]
    public async Task<IActionResult> UpdateCategory(int id)
    {
        if (await CheckAdmin())
            return RedirectToAction(nameof(Index), "Home");


        ViewBag.CategoryId = id;
        if (id != 0)
        {
            var fullModel = new Views
            {
               category = await _categoryService.GetOneCategoriesAsync(id)
            };
            return View(fullModel);
        }
        else
        {
            var fullModel = new Views
            {
                Categories = await _categoryService.GetAllCategoriesAsync()
            };
            return View(fullModel);
        }
    }

    [HttpPost("UpdateCategory")]
    public async Task<IActionResult> UpdateCategory(Category category)
    {
        if (!ModelState.IsValid)
            return View();

        if (await CheckAdmin())
            return RedirectToAction(nameof(Index), "Home");


        await _categoryService.UpdateCategoryAsync(category);

        return RedirectToAction("Admin", "Member");
    }


    [HttpGet("DeleteCategory")]
    public async Task<IActionResult> DeleteCategory()
    {
        if (await CheckAdmin())
            return RedirectToAction(nameof(Index), "Home");

        var fullModel = new Views
        {
            Categories = await _categoryService.GetAllCategoriesAsync()
        };
        return View(fullModel);
    }

    [HttpPost("DeleteCategory")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        Category category = await _categoryService.GetOneCategoriesAsync(id);

        await _categoryService.DeleteCategoryAsync(category);

        return RedirectToAction("Admin", "Member");
    }

    // SubCategory CRUD
    [HttpGet("CreateSubCategory")]
    public async Task<IActionResult> CreateSubCategory(int id)
    {
        ViewBag.CreateSubcategory = id;

        if (await CheckAdmin())
            return RedirectToAction(nameof(Index), "Home");

        var categories = await _categoryService.GetAllCategoriesAsync();


        if (id != 0)
        {
            var category = await _categoryService.GetOneCategoriesAsync(id);

            var fullModel = new Views
            {
                category = category,
            };
            return View(fullModel);
        }
        else
        {
            var fullModel = new Views
            {
               Categories = await _categoryService.GetAllCategoriesAsync()
            };
            return View(fullModel);
        }
    }

    [HttpPost("CreateSubCategory")]
    public async Task<IActionResult> CreateSubCategory(SubCategory subCategory)
    {
        if (!ModelState.IsValid)
            return View();

        await _categoryService.CreateSubCategoryAsync(subCategory);

        return RedirectToAction("Admin", "Member");
    }


    [HttpGet("UpdateSubCategory")]
    public async Task<IActionResult> UpdateSubCategory(int id)
    {
        if (await CheckAdmin())
            return RedirectToAction(nameof(Index), "Home");


        ViewBag.SubCategoryId = id;
        if (id != 0)
        {
            var subCategory = await _categoryService.GetOneSubCategoriesAsync(id);

            var fullModel = new Views
            {
                SubCategory = subCategory
            };
            return View(fullModel);
        }
        else
        {
            var fullModel = new Views
            {
                SubCategories = await _categoryService.GetAllSubCategoriesAsync()
            };
            return View(fullModel);
        }
    }

    [HttpPost("UpdateSubCategory")]
    public async Task<IActionResult> UpdateSubCategory(SubCategory subCategory)
    {
        if (!ModelState.IsValid)
            return View();

        if (await CheckAdmin())
            return RedirectToAction(nameof(Index), "Home");


        await _categoryService.UpdateSubCategoryAsync(subCategory);

        return RedirectToAction("Admin", "Member");
    }


    [HttpGet("DeleteSubCategory")]
    public async Task<IActionResult> DeleteSubCategory()
    {
        if (await CheckAdmin())
            return RedirectToAction(nameof(Index), "Home");

        var fullModel = new Views
        {
            SubCategories = await _categoryService.GetAllSubCategoriesAsync()
        };
        return View(fullModel);
    }

    [HttpPost("DeleteSubCategory")]
    public async Task<IActionResult> DeleteSubCategory(int id)
    {
        SubCategory subCategory = await _categoryService.GetOneSubCategoriesAsync(id);

        await _categoryService.DeleteSubCategoryAsync(subCategory);

        return RedirectToAction("Admin", "Member");
    }
}

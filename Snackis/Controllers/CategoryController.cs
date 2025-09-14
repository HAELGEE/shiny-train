using ApplicationService.Interface;
using Entity;
using Microsoft.AspNetCore.Mvc;

namespace Snackis.Controllers;
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }


    [HttpGet("CreateCategory")]
    public IActionResult CreateCategory()
    {
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
              
        await _categoryService.UpdateCategoryAsync(category);

        return RedirectToAction("Admin", "Member");
    }


    [HttpGet("DeleteCategory")]
    public async Task<IActionResult> DeleteCategory()
    {
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
}

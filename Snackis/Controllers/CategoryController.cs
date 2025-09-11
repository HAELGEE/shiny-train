using ApplicationService.Interface;
using EFCore;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Snackis.Controllers;
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }


    [HttpGet("Admin/Create")]
    public IActionResult Create()
    {
        
        return View();
    }

    [HttpPost("Admin/Create")]
    public async Task<IActionResult> Create(Category category)
    {
        if(!ModelState.IsValid) 
            return View();

        await _categoryService.CreateCategoryAsync(category);

        return View(category);
    }
}

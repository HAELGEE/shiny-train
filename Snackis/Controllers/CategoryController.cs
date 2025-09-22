using ApplicationService;
using ApplicationService.Interface;
using EFCore;
using Entity;
using Microsoft.AspNetCore.Mvc;

namespace Snackis.Controllers;
public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMemberService _memberService;
    private readonly IPostRepository _postRepository;

    public CategoryController(ICategoryRepository categoryRepository, IMemberService memberService, IPostRepository postRepository)
    {
        _categoryRepository = categoryRepository;
        _memberService = memberService;
        _postRepository = postRepository;
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
    [HttpGet("Subcategory")]
    public async Task<IActionResult> Subcategory(int id)
    {
        var subCategory = await _categoryRepository.GetOneSubCategoriesAsync(id);
        var posts = await _postRepository.GettingAllPostForSubCategoryAsync(id);


        var view = new Entities
        {
            SubCategory = subCategory,
            Posts = posts,
        };

        return View(view);
    }




    [HttpGet("CreateCategory")]
    public async Task<IActionResult> CreateCategory()
    {
        if (await CheckAdmin())
            return RedirectToAction(nameof(Index), "Home");

        return View();
    }

    [HttpPost("CreateCategory")]
    public async Task<IActionResult> CreateCategory(Category category)
    {
        if (!ModelState.IsValid)
            return View();

        var temp = await _categoryRepository.GetOneByNameCategoriesAsync(category.Name);

        if (temp != null)
        {
            ModelState.AddModelError("Name", "This Name is already taken.");
            return View();
        }

        await _categoryRepository.CreateCategoryAsync(category);

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
            var fullModel = new Entities
            {
                category = await _categoryRepository.GetOneCategoriesAsync(id)
            };
            return View(fullModel);
        }
        else
        {
            var fullModel = new Entities
            {
                Categories = await _categoryRepository.GetAllCategoriesAsync()
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

        var temp = await _categoryRepository.GetOneByNameCategoriesAsync(category.Name);

        if (temp != null)
        {
            ViewBag.CategoryId = category.Id;

            var fullModel = new Entities
            {
                Categories = await _categoryRepository.GetAllCategoriesAsync()
            };

            ModelState.AddModelError("Name", "This Name is already taken.");
            return View(fullModel);
        }

        await _categoryRepository.UpdateCategoryAsync(category);

        return RedirectToAction("Admin", "Member");
    }


    [HttpGet("DeleteCategory")]
    public async Task<IActionResult> DeleteCategory()
    {
        if (await CheckAdmin())
            return RedirectToAction(nameof(Index), "Home");

        var fullModel = new Entities
        {
            Categories = await _categoryRepository.GetAllCategoriesAsync()
        };
        return View(fullModel);
    }

    [HttpPost("DeleteCategory")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        Category category = await _categoryRepository.GetOneCategoriesAsync(id);

        try
        {
            await _categoryRepository.DeleteCategoryAsync(category);
        }
        catch (Exception ex)
        {
            return RedirectToAction("Admin", "Member", new { warningText = $"No access to delete categories and subcategories when post is in it" });
        }

        return RedirectToAction("Admin", "Member");
    }

    // SubCategory CRUD
    [HttpGet("CreateSubCategory")]
    public async Task<IActionResult> CreateSubCategory(int id)
    {
        ViewBag.CreateSubcategory = id;

        if (await CheckAdmin())
            return RedirectToAction(nameof(Index), "Home");

        var categories = await _categoryRepository.GetAllCategoriesAsync();


        if (id != 0)
        {
            var category = await _categoryRepository.GetOneCategoriesAsync(id);

            var fullModel = new Entities
            {
                category = category,
            };
            return View(fullModel);
        }
        else
        {
            var fullModel = new Entities
            {
                Categories = await _categoryRepository.GetAllCategoriesAsync()
            };
            return View(fullModel);
        }
    }

    [HttpPost("CreateSubCategory")]
    public async Task<IActionResult> CreateSubCategory(SubCategory subCategory)
    {
        if (!ModelState.IsValid)
            return View();

        var temp = await _categoryRepository.GetOneByNameSubCategoriesAsync(subCategory.Name);

        if (temp != null)
        {
            ViewBag.CreateSubcategory = subCategory.CategoryId;
            var category = await _categoryRepository.GetOneCategoriesAsync((int)subCategory.CategoryId);

            var fullModel = new Entities
            {
                category = category,
            };

            ModelState.AddModelError("Name", "This Name is already taken.");
            return View(fullModel);
        }

        await _categoryRepository.CreateSubCategoryAsync(subCategory);

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
            var subCategory = await _categoryRepository.GetOneSubCategoriesAsync(id);

            var fullModel = new Entities
            {
                SubCategory = subCategory
            };
            return View(fullModel);
        }
        else
        {
            var fullModel = new Entities
            {
                SubCategories = await _categoryRepository.GetAllSubCategoriesAsync()
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

        var temp = await _categoryRepository.GetOneByNameSubCategoriesAsync(subCategory.Name);

        if (temp != null)
        {
            ViewBag.SubCategoryId = subCategory.Id;

            var fullModel = new Entities
            {
                SubCategories = await _categoryRepository.GetAllSubCategoriesAsync()
            };

            ModelState.AddModelError("Name", "This Name is already taken.");
            return View(fullModel);
        }

        await _categoryRepository.UpdateSubCategoryAsync(subCategory);

        return RedirectToAction("Admin", "Member");
    }


    [HttpGet("DeleteSubCategory")]
    public async Task<IActionResult> DeleteSubCategory()
    {
        if (await CheckAdmin())
            return RedirectToAction(nameof(Index), "Home");

        var fullModel = new Entities
        {
            SubCategories = await _categoryRepository.GetAllSubCategoriesAsync()
        };
        return View(fullModel);
    }

    [HttpPost("DeleteSubCategory")]
    public async Task<IActionResult> DeleteSubCategory(int id)
    {
        SubCategory subCategory = await _categoryRepository.GetOneSubCategoriesAsync(id);

        try
        {
            await _categoryRepository.DeleteSubCategoryAsync(subCategory);
        }
        catch (Exception ex)
        {
            return RedirectToAction("Admin", "Member", new { warningText = $"No access to delete categories and subcategories when post is in it" });
        }

        return RedirectToAction("Admin", "Member");
    }
}

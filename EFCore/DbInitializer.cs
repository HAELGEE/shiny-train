using ApplicationService;
using ApplicationService.Interface;
using EFCore;
using Entity;
using System;

public static class DbInitializer
{
    public static async Task SeedAsync(ICategoryService categoryService, IMemberService memberService)
    {
        var members = await memberService.GetAllMembersAsync();
        if (!members.Any())
        {
            var admin = new Member
            {
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@admin.se",
                Birthday = "11111111",
                IsAdmin = true,
                IsOwner = true,
                UserName = "Admin",
                Password = "password123!" // Viktigt: hasha
            };

            await memberService.CreateMemberAsync(admin);
        }

        var categories = await categoryService.GetAllCategoriesAsync();
        if (!categories.Any())
        {
            await categoryService.CreateCategoryAsync(new Category { Name = "Cars" });
            await categoryService.CreateCategoryAsync(new Category { Name = "Hobbies" });
            await categoryService.CreateCategoryAsync(new Category { Name = "Gaming" });
        }

        var subcategories = await categoryService.GetAllSubCategoriesAsync();
        if (!subcategories.Any())
        {
            await categoryService.CreateSubCategoryAsync(new SubCategory            
            {
                CategoryId = 1,
                Name = "Volvo",

            });
            await categoryService.CreateSubCategoryAsync(new SubCategory
            {
                CategoryId = 2,
                Name = "Football",
            });
            await categoryService.CreateSubCategoryAsync(new SubCategory
            {
                CategoryId = 3,
                Name = "Dota2",
            });
        }
    }
}
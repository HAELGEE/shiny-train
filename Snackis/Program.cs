using ApplicationService;
using ApplicationService.Interface;
using EFCore;
using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Snackis;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<MyDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbContext"))
        );


        builder.Services.AddScoped<IMemberRepository, MemberRepository>();
        builder.Services.AddScoped<IMemberService, MemberService>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IPostService, PostService>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<IReportsRepository, ReportsRepository>();
        builder.Services.AddScoped<IHomeRepository, HomeRepository>();
        builder.Services.AddScoped<IHomeService, HomeService>();
        builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
        builder.Services.AddScoped<IConversationService, ConversationService>();


        builder.Services.AddSession();


        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }


        app.UseSession();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseStaticFiles();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}

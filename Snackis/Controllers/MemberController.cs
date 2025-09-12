using ApplicationService.Interface;
using EFCore;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Snackis.Controllers;
public class MemberController : Controller
{
    private readonly IMemberService _memberService;

    public MemberController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> Profile()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null || userId == 0)
            return RedirectToAction(nameof(Register), "Member");

        FullViewModel viewModel = new FullViewModel();
        
        var member = await _memberService.GetOneMemberAsync((int)userId);
        
        if (member != null)
        {
            ViewBag.Member = member;
            int? birthDateInt = member.Age;
            DateTime birthDate = DateTime.ParseExact(
                birthDateInt.ToString(),
                "yyyymmdd",
                System.Globalization.CultureInfo.InvariantCulture
                );

            DateTime today = DateTime.Today;

            int age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
                age--;

            member.Age = age;

            viewModel.Member = member;
        }

        //_memberService.GetOneMemberAsync(member.Id);

        return View(viewModel);
    }

    [HttpPost("profile")]
    public IActionResult Profile(Member member)
    {
        if(member.IsAdmin)
            HttpContext.Session.SetInt32("IsAdmin" , 1) ; //This is to create a session variable to access the Admin page

        return View();
    }



    [HttpGet("Register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(Member member)
    {
        
        if (await _memberService.GetMemberByEmailAsync(member.Email) != null)
        {
            ModelState.AddModelError("Email", "This email is already taken.");
            return View();
        }

        if (!ModelState.IsValid)  
            return View();

        await _memberService.CreateMemberAsync(member);
        HttpContext.Session.SetInt32("UserId", member.Id);
        HttpContext.Session.SetString("UserName", member.UserName);

        return RedirectToAction(nameof(Index), "Home");        
    }


    [HttpGet("admin")]
    public async Task<IActionResult> Admin()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return RedirectToAction(nameof(Index), "Home");

        var member = await _memberService.GetOneMemberAsync((int)userId);
        
        if(member == null)
            return RedirectToAction(nameof(Index), "Home");

        if(!member.IsAdmin)
            return RedirectToAction(nameof(Index), "Home");

        return View(member);
    }

    [HttpGet("Login")]
    public IActionResult Login()
    {

        var userName = HttpContext.Session.GetString("UserName");

        if (!string.IsNullOrWhiteSpace(userName))
        {
            HttpContext.Session.SetString("UserName", "");
            HttpContext.Session.SetInt32("UserId", 0);

            return RedirectToAction(nameof(Index), "Home");
        }

        return View();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(string UserName, string Password)
    {
        var member = await _memberService.GetMemberByUsernamePasswordAsync(UserName, Password);

        if(member == null)
        {
            ModelState.AddModelError("", "Wrong Username or Password");
            return View();
        }

        ViewData["UserId"] = member.Id;
        ViewData["UserName"] = member.UserName;
        HttpContext.Session.SetInt32("UserId", member.Id);
        HttpContext.Session.SetString("UserName", member.UserName);

        return RedirectToAction(nameof(Profile), "Member");
    }

    [HttpGet("Update")]
    public async Task<IActionResult> Update(int Id)
    {
        var member = await _memberService.GetOneMemberAsync(Id);

        return View(member);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update(Member member)
    {
        if(!ModelState.IsValid)
            return View(new { member });

        return View();
    }
}
    
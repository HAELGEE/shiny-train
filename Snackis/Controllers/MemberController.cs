using ApplicationService.Interface;
using EFCore;
using Entity;
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

        if (userId == null)
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

    [HttpGet("Member/SignIn")]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost("Member/SignIn")]
    public async Task<IActionResult> SignIn(string username, string password)
    {
        var member = await _memberService.GetMemberByUsernamePasswordAsync(username, password);

        if(member == null)
        {
            ModelState.AddModelError("", "Wrong Username or Password");
            return View();
        }

        HttpContext.Session.SetInt32("UserId", member.Id);
        HttpContext.Session.SetString("UserName", member.UserName);

        return RedirectToAction(nameof(Profile), "Member");
    }
    
}
    
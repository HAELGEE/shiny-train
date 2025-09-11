using ApplicationService.Interface;
using EFCore;
using Entity;
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
    public IActionResult Profile(Member member)
    {
        if (ViewData["IsLoggedIn"] == "false")
            return RedirectToAction(nameof(Register), "Member");


        _memberService.GetOneMemberAsync(member.Id);
        return View();
    }

    [HttpPost("profile")]
    public IActionResult Profile()
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
        if (_memberService.GetMemberByEmailAsync(member.Email) != null)
        {
            ModelState.AddModelError("Email", "This email is already taken.");
            return View();
        }

        if (!ModelState.IsValid)  
            return View();

        await _memberService.CreateMemberAsync(member);       

        return RedirectToAction(nameof(Index), "Home");
        
    }

    [HttpGet("admin")]
    public IActionResult Admin()
    {
        return View();
    }

    
}
    
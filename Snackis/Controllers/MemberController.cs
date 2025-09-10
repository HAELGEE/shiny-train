using ApplicationService.Interface;
using EFCore;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Snackis.Controllers;
public class MemberController : Controller
{
    private readonly MyDbContext _context;

    public MemberController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet("Register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(Member member)
    {
        if (_context.Member.Any(m => m.Email == member.Email))
        {
            ModelState.AddModelError("Email", "This email is already taken.");
            return View();
        }


        if (!ModelState.IsValid)  
            return View();



        _context.Add(member);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index), "Home");
        
    }
}
    
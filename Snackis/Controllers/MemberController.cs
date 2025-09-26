using ApplicationService.Interface;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Snackis.Controllers;

public class MemberController : Controller
{
    private readonly IMemberService _memberService;
    private readonly IPostService _postService;

    public MemberController(IMemberService memberService, IPostService postService)
    {
        _memberService = memberService;
        _postService = postService;
    }

    async Task<bool> IsAdmin()
    {
        var members = await _memberService.GetAdminMembersAsync();
        bool adminCheck = false;
        foreach (var member in members)
        {
            if (member.IsAdmin == true)
            {
                adminCheck = true; break;
            }
        }
        return adminCheck;
    }


    [HttpGet("profile")]
    public async Task<IActionResult> Profile(int receiverId, Chatt chatt)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null || userId == 0)
            return RedirectToAction(nameof(Login), "Member");

       


        var member = await _memberService.GetOneMemberAsync((int)userId);

        FullViewModel viewModel = new FullViewModel();
        if (member != null)
        {
            ViewBag.Member = member;

            viewModel.Member = member;
            viewModel.Chatts = await _memberService.AllChatForMemberAsync((int)userId);

            viewModel.Chat = new Chatt();


            if ((int)userId > 0 && receiverId > 0)
            {
                viewModel.ChattMessages = await _memberService.GetAllChattMessagesFromReceiverIdAsync((int)userId, receiverId);
                viewModel.ReceiverMemberID = receiverId;
            }

        }

        return View(viewModel);
    }
    [HttpPost("profile")]
    public async Task<IActionResult> Profile(Member member, FullViewModel fullViewModel)
    {
        string filename = "";

        if (fullViewModel.UploadedImage != null)
        {
            // Valfritt sätt att göra bildnamnet unikt
            filename = Random.Shared.Next(0, 99999).ToString() + "_" + $"ID={HttpContext.Session.GetInt32("UserId")}"
                + "_" + fullViewModel.UploadedImage.FileName;

            using (var fileStream = new FileStream("./wwwroot/uploads/" + filename, FileMode.Create))
            {
                await fullViewModel.UploadedImage.CopyToAsync(fileStream);
            }
            var userMember = await _memberService.GetOneMemberAsync((int)HttpContext.Session.GetInt32("UserId"));

            string pictureId = userMember.ProfileImagePath;

            userMember.ProfileImagePath = "/uploads/" + filename;
            
            await _memberService.UpdateMemberAsync(userMember, pictureId);
        }

        fullViewModel.Member = member;

        return RedirectToAction(nameof(Profile));
    }

    [HttpPost("MakeAdmin")]
    public async Task<IActionResult> MakeAdmin(Member member)
    {
        var isAdminCheck = await IsAdmin();

        if (isAdminCheck)
        {
            await _memberService.UpdateMemberAdminrightsAsync(member.Id, member.IsAdmin);
        }

        return RedirectToAction(nameof(Guest), new { id = member.Id });
    }


    [HttpGet("Guest")]
    public async Task<IActionResult> Guest(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null || userId == 0)
            return RedirectToAction(nameof(Login), "Member");

        if (userId == id || id == 0)
            return RedirectToAction(nameof(Profile), "Member");


        var member = await _memberService.GetOneMemberAsync(id);


        var view = new Entities
        {
            Member = member
        };

        Member? admin = null;
        if (await _memberService.GetAdminMemberAsync((int)HttpContext.Session.GetInt32("UserId")!) != null)
        {
            admin = await _memberService.GetAdminMemberAsync((int)HttpContext.Session.GetInt32("UserId")!);

            view.Admin = admin;
        }

        await _memberService.AddProfileViewAsync((int)userId, member.Id);

        return View(view);
    }

    [HttpGet("memberWarning")]
    public async Task<IActionResult> memberWarning(int id)
    {
        bool isAdmin = await IsAdmin();

        if (!isAdmin)
            return RedirectToAction(nameof(Index), "Home");

        await _memberService.UpdateReportsForMemberAsync(id);

        return RedirectToAction(nameof(Guest), "Member", new { Id = id });
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

        if (await _memberService.GetMemberByUsernameAsync(member.UserName) != null)
        {
            ModelState.AddModelError("UserName", "This Username is already taken.");
            return View();
        }

        if (!ModelState.IsValid)
            return View();

        await _memberService.CreateMemberAsync(member);
        HttpContext.Session.SetInt32("UserId", member.Id);
        HttpContext.Session.SetString("UserName", member.UserName);
        HttpContext.Session.SetInt32("IsAdmin", 0);


        return RedirectToAction(nameof(Index), "Home");
    }

    [HttpGet("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword(string email, string password)
    {
        var newMember = new Member();

        if (email != null)
        {
            newMember = await _memberService.GetMemberByEmailAsync(email);
        }

        var view = new Entities
        {
            Password = password,
            Member = newMember,
            Members = await _memberService.GetAllMembersAsync()
        };

        return View(view);
    }
    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword(Member member)
    {
        char Pick(string set, RandomNumberGenerator rng)
        {
            int idx = RandomNumberGenerator.GetInt32(set.Length);
            return set[idx];
        }


        char[] newPassword = new char[10];
        if (member.Email != null)
        {
            string Lower = "abcdefghijklmnopqrstuvwxyz";
            string Upper = Lower.ToUpper();
            string Digits = "0123456789";
            string All = Lower + Upper + Digits;


            var rng = RandomNumberGenerator.Create();
            newPassword[0] = Pick(Lower, rng);  // Always lower to start
            newPassword[1] = Pick(Upper, rng);  // Always upper for second
            newPassword[2] = Pick(Digits, rng); // Always one number at third


            for (int i = 3; i < newPassword.Length; i++)
                newPassword[i] = Pick(All, rng);

            var newMember = await _memberService.GetMemberByEmailAsync(member.Email);


            newMember.Password = new string(newPassword);

            await _memberService.UpdateMemberAsync(newMember, null);
        }
        return RedirectToAction(nameof(ForgotPassword), new { password = new string(newPassword) });
    }

    [HttpGet("Admin")]
    public async Task<IActionResult> Admin(string warningText)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return RedirectToAction(nameof(Index), "Home");

        var member = await _memberService.GetOneMemberAsync((int)userId);

        if (member == null)
            return RedirectToAction(nameof(Index), "Home");

        if (!member.IsAdmin)
            return RedirectToAction(nameof(Index), "Home");

        var posts = await _postService.GetAllReportsAsync();
        var members = await _memberService.GetAllMembersAsync();

        var reports = await _memberService.GettingAllReportsAsync();

        foreach (var post in posts)
        {
            foreach (var report in reports)
            {
                if (post.Id == report.PostId)
                {
                    post.TotalReports++;
                }
            }
        }

        var view = new Entities
        {
            WarningMessage = warningText,
            Posts = posts,
            Members = members,
        };

        return View(view);
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

        if (member == null)
        {
            ModelState.AddModelError("Error", "Wrong Username or Password");
            return View();
        }

        HttpContext.Session.SetInt32("UserId", member.Id);
        HttpContext.Session.SetString("UserName", member.UserName);
        HttpContext.Session.SetInt32("updateSubpost", 0);

        if (member.IsAdmin)
            HttpContext.Session.SetInt32("IsAdmin", 1); //This is to create a session variable to access the Admin page
        else
            HttpContext.Session.SetInt32("IsAdmin", 0); //This is to create a session variable to access the Admin page

        return RedirectToAction(nameof(Profile), "Member");
    }

    [HttpGet("Update")]
    public async Task<IActionResult> Update(int Id)
    {
        var member = await _memberService.GetOneMemberAsync(Id);

        return View(member);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update(int id, Member member)
    {
        if (!ModelState.IsValid)
            return View();

        member.Id = id;


        await _memberService.UpdateMemberAsync(member, null);

        return RedirectToAction(nameof(Profile), "Member");
    }

    [HttpPost("CreateChatt")]
    public async Task<IActionResult> CreateChatt(Chatt chat, string userName)
    {
        if (chat.SenderId > 0 && userName != null)
        {
            chat.SenderMember = await _memberService.GetOneMemberAsync((int)chat.SenderId);
            chat.ReceiverMember = await _memberService.GetMemberByUsernameAsync(userName);
            await _memberService.CreateChattWithUserAsync(chat);
        }
        return RedirectToAction(nameof(Profile), new { receiverId = chat.ReceiverId });
    }

    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessage(Chatt chat)
    {
        if (chat.SenderId > 0 && chat.ReceiverId > 0)
        {
            chat.SenderMember = await _memberService.GetOneMemberAsync((int)chat.SenderId);
            chat.ReceiverMember = await _memberService.GetOneMemberAsync((int)chat.ReceiverId);
            await _memberService.CreateChattWithUserAsync(chat);
        }

        return RedirectToAction(nameof(Profile), new { receiverId = chat.ReceiverId });
    }
}

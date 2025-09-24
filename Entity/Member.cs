using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Entity;

[Index(nameof(UserName), nameof(Email), IsUnique = true)]
public class Member
{
    public int Id { get; set; }

    // Some criteria for user registration
    [Display(Name = "First name", Prompt = "Insert First name here...")]
    [Required(ErrorMessage = "Must type in your First name")]
    public string? FirstName { get; set; }

    [Display(Name = "Last name", Prompt = "Insert Last name here...")]
    [Required(ErrorMessage = "Must type in your Last name")]
    public string? LastName { get; set; }

    //[RegularExpression(@"^\d{8}$", ErrorMessage = "Date must be as YYYYMMDD.")]
    [Display(Name = "Birthday", Prompt = "Insert yyyymmdd here...")]
    [Required(ErrorMessage = "Must type in your Birthday")]
    [ValidDate(ErrorMessage = "Date must be a valid date in format YYYYMMDD.")]
    public string? Birthday { get; set; }

    [Display(Name = "Username", Prompt = "Insert Username here...")]
    [Required(ErrorMessage = "Must type in your Username")]
    public string? UserName { get; set; }

    [EmailAddress(ErrorMessage = "Must be a legit Email-Address")]
    [Display(Name = "Email", Prompt = "Insert Email-address here...")]
    [Required]
    public string? Email { get; set; }

    [Display(Name = "Password", Prompt = "Insert your Password here...")]
    [Required(ErrorMessage = "Must type in a Password")]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "You did not match the passwords")]
    [Display(Name = "Re-Password", Prompt = "Insert your Password here...")]
    [Required()]
    public string? PasswordValidation { get; set; }

    // Admin rights
    public bool IsOwner { get; set; } = false;
    public bool IsAdmin { get; set; } = false;
    public int? Reports { get; set; } = 0;

    // Information for the View on Profile
    public int Age
    {
        get
        {
            // Theses controlls are just because this executes before the validation, so i typed these here to prevent error
            if (!string.IsNullOrEmpty(Birthday) && 
                DateTime.TryParseExact(Birthday, "yyyyMMdd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var birthDate))
            {
                var age = DateTime.Now.Year - birthDate.Year;
                if (DateTime.Now < birthDate.AddYears(age)) age--;
                return age;
            }
            else
                return 0;
        }           
    }

    public int TotalPosts { get; set; } = 0;
    public int TotalReply { get; set; } = 0;
    public DateTime? RegisteryDate { get; set; } = DateTime.Now;
    public string ProfileImagePath { get; set; } = "/uploads/standardProfile.png";


    // DB connections 
    public ICollection<MemberView>? MemberViews { get; set; }
    public ICollection<PostView>? PostViews { get; set; }
    public ICollection<Reports>? ReportedPosts { get; set; }
    public ICollection<Post>? Posts { get; set; }
    public ICollection<SubPost>? SubPosts { get; set; }
    public ICollection<Likes>? Likes { get; set; }
    public ICollection<Chatt>? Chatt { get; set; }

}
public class ValidDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null) return false;

        string strValue = value.ToString()!;

        return DateTime.TryParseExact(
            strValue,
            "yyyyMMdd",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out _
        );
    }
}

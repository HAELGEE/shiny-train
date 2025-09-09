using System.ComponentModel.DataAnnotations;

namespace Entity;

public class Member
{
    public int Id { get; set; }

    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public int? Age { get; set; }

    [Required]
    public string? UserName { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    
    public bool IsAdmin { get; set; } = false;


    public int TotalPosts { get; set; } = 0;
    public int TotalReply { get; set; } = 0;
    public int ProfileViews { get; set; } = 0;


}

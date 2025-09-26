using Microsoft.AspNetCore.Http;

namespace Entity;
public class Entities
{
    public Member? Member { get; set; }
    public List<Member>? Members { get; set; }
    public Category? category { get; set; } = new();
    public List<Category>? Categories { get; set; } = new();
    public SubCategory? SubCategory { get; set; } = new();
    public List<SubCategory>? SubCategories { get; set; }

    public List<Member>? Admins { get; set; } = new();
    public Member? Admin { get; set; } = new();
    public Post? Post { get; set; }

    public List<Post>? Posts { get; set; }
    
    public List<Post>? RecentPosts { get; set; } = new();

    public SubPost? SubPost { get; set; }
    public List<SubPost>? SubPosts { get; set; }   

    public string? Password { get; set; }
    public string? WarningMessage { get; set; }

    


}

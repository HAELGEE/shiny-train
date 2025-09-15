namespace Entity;
public class Views
{
    public Member? Member { get; set; }
    public Category? category { get; set; } = new();
    public List<Category>? Categories { get; set; } = new();
    public SubCategory? SubCategory { get; set; } = new();
    public List<SubCategory>? SubCategories { get; set; }

    public Post? Post { get; set; }

    public List<Post>? Posts { get; set; }

    public List<Post>? RecentPosts { get; set; } = new();

    public SubPost? SubPost { get; set; }
    public List<SubPost>? SubPosts { get; set; }   


   
}

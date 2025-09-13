namespace Entity;
public class Views
{
    public Member? Member { get; set; }
    public Category? category { get; set; } = new();
    public List<Category>? Categories { get; set; } = new();
    public List<SubCategory>? SubCategorys { get; set; }
    public List<Post>? Posts { get; set; }
    public List<Post>? RecentPosts { get; set; } = new();

    public List<SubPost>? SubPosts { get; set; }   


   
}

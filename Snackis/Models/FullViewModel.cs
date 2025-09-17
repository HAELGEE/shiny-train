using ApplicationService.Interface;

namespace Entity;
public class FullViewModel
{
    public Member? Member { get; set; }
    public List<Category>? Categorys { get; set; } = new();
    public List<SubCategory>? SubCategorys { get; set; }
    public List<Post>? Posts { get; set; }
    public List<Post>? Top10Posts { get; set; }
    public List<SubPost>? SubPosts { get; set; }    


}

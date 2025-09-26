using ApplicationService.Interface;

namespace Entity;
public class FullViewModel
{
    public Member? Member { get; set; }
    public List<Category>? Categorys { get; set; } = new();
    public List<SubCategory>? SubCategorys { get; set; }
    public List<Post>? Posts { get; set; }
    public Post? Post { get; set; }
    public List<Post>? Top10Posts { get; set; }
    public List<SubPost>? SubPosts { get; set; }
    public List<Chatt>? Chatts { get; set; } = new();
    public List<Chatt>? ChattsByReceiver { get; set; }
    public List<Chatt>? ChattMessages { get; set; }
    public Chatt? Chat {  get; set; }
    public int? ReceiverMemberID { get; set; }
    public IFormFile? UploadedImage { get; set; }

}

namespace Entity;
public class Likes
{
    public int Id { get; set; }

    public int? MemberId { get; set; }
    public Member? Member { get; set; }

    public int? PostId { get; set; }
    public Post? Post { get; set; }
}

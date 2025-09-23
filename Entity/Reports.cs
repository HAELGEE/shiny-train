namespace Entity;

public class Reports
{
    public int Id { get; set; }

    public int? MemberId { get; set; }
    public Member? Member { get; set; }

    public int? PostId { get; set; }
    public Post? Post { get; set; }
}

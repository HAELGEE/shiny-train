namespace Entity;

public class Chatt
{
    public int Id { get; set; }
    public int? SenderId { get; set; }
    public int? ReceiverId { get; set; }
    public string? Text { get; set; }
    public DateTime TimeCreated { get; set; }

    // DB connections
    public ICollection<Member>? Member { get; set; }

}

namespace Entity;

public class Chatt
{
    public int Id { get; set; }
    public int? SenderId { get; set; }
    public Member SenderMember { get; set; } = new();
    public int? ReceiverId { get; set; }
    public Member ReceiverMember { get; set; } = new();
    public string? Text { get; set; }
    public DateTime TimeCreated { get; set; }

    

}

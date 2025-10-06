using System.ComponentModel.DataAnnotations;

namespace Entity;
public class SubPost
{
    public int Id { get; set; }

    [Display(Name = "Reply text", Prompt = "Insert text here...")]
    [Required]
    public string Text { get; set; }
    public string? ReplyText { get; set; }
    public int? ParentPostId { get; set; }
    public int? ParentSubpostId { get; set; }
    public bool Reported { get; set; } = false;
    public int? ReporterId { get; set; }
    public int TotalReports { get; set; } = 0;
    public string? ImagePath { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;


    // DB connections
    public int? PostId { get; set; }
    public int? MemberId { get; set; }
    public Member? Member { get; set; }
    public ICollection<Reports>? ReporterIds { get; set; }

}

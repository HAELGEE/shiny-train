using System.ComponentModel.DataAnnotations;

namespace Entity;
public class SubPost
{
    public int Id { get; set; }

    [Display(Name = "Reply text", Prompt = "Insert text here...")]
    [Required]
    public string? Text { get; set; }
    public bool? Reported { get; set; } = false;

    public DateTime? Created { get; set; } = DateTime.Now;


    // DB connections
    public int? PostId { get; set; }
    public int? MemberId { get; set; }
    public Member? Member { get; set; }

}

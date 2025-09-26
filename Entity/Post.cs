using System.ComponentModel.DataAnnotations;

namespace Entity;
public class Post
{
    public int Id { get; set; }

    [Display(Name = "Description", Prompt = "Insert description here...")]
    [Required]
    public string Description { get; set; } // What the Post is about (short description)

    [Display(Name = "Text", Prompt = "Insert text here...")]
    [Required]
    public string Text { get; set; }   // Text (information about the context)

    public string? ImagePath { get; set; }

    public int Reply { get; set; } = 0;
    public bool Reported { get; set; } = false;
    public int? ReporterId { get; set; }
    public int TotalReports { get; set; } = 0;

    public DateTime Created { get; set; } = DateTime.UtcNow;
   

    // DB connections
    public int? SubCategoryId { get; set; }
    public SubCategory? SubCategory { get; set; }
    public int? MemberId { get; set; }
    public Member? Member { get; set; }
    public ICollection<SubPost>? SubPosts { get; set; }
    public ICollection<PostView>? Views { get; set; }
    public ICollection<Reports>? ReporterIds { get; set; }
    public ICollection<Likes>? Likes { get; set; }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity;
public class Post
{
    public int Id { get; set; }

    [Display(Name = "Description", Prompt = "Insert description here...")]
    [Required]
    public string? Description { get; set; } // What the Post is about (short description)

    [Display(Name = "Text", Prompt = "Insert text here...")]
    [Required]
    public string? Text { get; set; }   // Text (information about the context)

    public int Views { get; set; } = 0;
    public int Like { get; set; } = 0;
    public int Reply { get; set; } = 0;
    public bool? Reported { get; set; } = false;
    public int? ReporterId { get; set; }
    public DateTime? Created { get; set; } = DateTime.Now;


    // DB connections
    public int? SubCategoryId { get; set; }
    public int? MemberId { get; set; }
    public Member? Member { get; set; }
    public ICollection<SubPost>? SubPosts { get; set; }
    public ICollection<Likes>? Likes { get; set; }

}

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

    [Required]
    public string? Description { get; set; } // What the Post is about (short description)

    [Required]
    public string? Text { get; set; }   // Text (information about the context)

    public int Views { get; set; } = 0;
    public int Likes { get; set; } = 0;
    public int Reply { get; set; } = 0;
    public DateTime? Created { get; set; } = DateTime.Now;


    // DB connections
    public int? SubCategoryId { get; set; }
    public int? MemberId { get; set; }
    public Member? Member { get; set; }
    public ICollection<SubPost>? SubPosts { get; set; }

}

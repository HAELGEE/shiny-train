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
    public string? Description { get; set; } // namnet på postinlägget
    [Required]
    public string? Text { get; set; }   // Texten som skall informeras ut

    public int Views { get; set; } = 0;
    public int Likes { get; set; } = 0;
    public int Reply { get; set; } = 0;

    public int? SubCategoryId { get; set; }
    public ICollection<SubPost>? SubPosts { get; set; }

}

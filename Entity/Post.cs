using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity;
public class Post
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Text { get; set; }

    public int? SubCategoryId { get; set; }
    public ICollection<SubPost>? SubPosts { get; set; }

}

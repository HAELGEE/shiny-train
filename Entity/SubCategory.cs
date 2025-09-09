using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity;
public class SubCategory
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public int? CategoryId { get; set; }
    public ICollection<Post>? Posts { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity;
public class SubCategory
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }


    // DB connections
    public int? CategoryId { get; set; }
    public ICollection<Post>? Posts { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity;
public class Category
{
    public int Id { get; set; }
    [Display(Name = "Name of Category", Prompt = "Insert the name here...")]
    [Required]
    public string? Name { get; set; }

    public ICollection<SubCategory>? SubCategories { get; set; }
}

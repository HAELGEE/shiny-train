using System.ComponentModel.DataAnnotations;

namespace Entity;
public class Category
{
    public int Id { get; set; }

    [Display(Name = "Name of Category", Prompt = "Insert the name here...")]
    [Required]
    public string? Name { get; set; }

    
    // DB connections
    public ICollection<SubCategory>? SubCategories { get; set; }
}

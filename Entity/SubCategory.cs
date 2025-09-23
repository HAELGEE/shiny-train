using System.ComponentModel.DataAnnotations;

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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity;
public class SubPost
{
    public int Id { get; set; }

    [Display(Name = "Reply text", Prompt = "Insert text here...")]
    [Required(ErrorMessage = "Must type in text to reply")]
    public string? Text { get; set; }
    public bool? Reported { get; set; } = false;

    public DateTime? Created { get; set; } = DateTime.Now;


    // DB connections
    public int? PostId { get; set; }
    public int? MemberId { get; set; }
    public Member? Member { get; set; }

}

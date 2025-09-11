using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity;
public class FullViewModel
{
    public Member? Member { get; set; }
    public Category? Category { get; set; }
    public SubCategory? SubCategory { get; set; }
    public Post? Post { get; set; }
    public SubPost? SubPost { get; set; }
}

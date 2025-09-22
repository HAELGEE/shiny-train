using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity;
public class MemberView
{
    public int Id { get; set; }

    public int? MemberId { get; set; }
    public Member? Member { get; set; }

    public int? VisitorId { get; set; }
    public Member? Visitor { get; set; }
}

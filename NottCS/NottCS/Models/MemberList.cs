using System;
using System.Collections.Generic;
using System.Text;

namespace NottCS.Models
{
    public class MemberList
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<Member> Results { get; set; }
    }
}

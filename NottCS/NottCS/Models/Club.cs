using System;
using System.Collections.Generic;
using System.Text;

namespace NottCS.Models
{
    internal abstract class Club
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Uri LogoUri { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NottCS.Models
{
    public class Club
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public DateTime CreatedTimeStamp { get; set; }

        public DateTime UpdatedTimeStamp { get; set; }
    }
}
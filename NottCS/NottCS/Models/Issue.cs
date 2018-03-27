using System;
using System.Collections.Generic;
using System.Text;

namespace NottCS.Models
{
    internal class Issue
    {
        public string ID { get; set; } // Issue Number/ID

        public string Username { get; set; }   // Complainer

        public string Feedback { get; set; }    // User Feedback

        public string Problem { get; set; }       // User Problem/Issue
    }
}

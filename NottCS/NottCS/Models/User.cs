using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NottCS.Models
{
    internal class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LibraryNumber { get; set; }
        public string StudentId { get; set; }
        public string Course { get; set; }

        public User()
        {
            Debug.WriteLine("Calling the default constructor for User");
            Username = "null";
            Name = "null";
            LibraryNumber = "null";
            StudentId = "null";
            Course = "null";
        }
    }
}

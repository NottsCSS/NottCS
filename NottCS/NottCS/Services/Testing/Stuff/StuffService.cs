using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NottCS.Services.Stuff
{
    internal class StuffService : IStuffService
    {
        private string _name = "HELLO";
        public string Name { get => _name; set => _name = value; }

        public void SetStuff(int a)
        {
            Debug.WriteLine(a);
        }
    }
}

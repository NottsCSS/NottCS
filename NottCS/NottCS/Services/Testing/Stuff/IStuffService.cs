using System;
using System.Collections.Generic;
using System.Text;

namespace NottCS.Services.Stuff
{
    public interface IStuffService
    {
        string Name { get; set; }
        void SetStuff(int a);
    }
}

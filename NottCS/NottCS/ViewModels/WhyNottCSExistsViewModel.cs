using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NottCS.ViewModels
{
    internal class WhyNottCSExistsViewModel: BaseViewModel
    {
        public WhyNottCSExistsViewModel()
        {
            Debug.WriteLine("Constructor is called");
            Title = "About";
        }
    }
}

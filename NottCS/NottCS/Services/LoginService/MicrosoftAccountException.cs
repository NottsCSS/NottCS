using System;
using System.Collections.Generic;
using System.Text;

namespace NottCS.Services.LoginService
{
    public class MicrosoftAccountException : Exception
    {
        public MicrosoftAccountException(string s) : base(s) { }
    }
}

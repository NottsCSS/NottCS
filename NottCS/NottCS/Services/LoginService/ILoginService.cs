using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace NottCS.Services.LoginService
{
    public interface ILoginService
    {
        Task<AuthenticationResult> SignInAsync();
        Task SignOutAsync();
    }
}

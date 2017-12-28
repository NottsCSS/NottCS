using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NottCS.ViewModels
{
    class ForgotPasswordViewModel : BaseViewModel
    {
        public ForgotPasswordViewModel()
        {
            Title = "Forgot Password";
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }
    }
}

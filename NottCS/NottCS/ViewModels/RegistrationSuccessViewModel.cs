using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NottCS.ViewModels
{

    class RegistrationSuccessViewModel : BaseViewModel
    {
        public RegistrationSuccessViewModel()
        {
            Title = "Registration Successful";
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }
    }
}

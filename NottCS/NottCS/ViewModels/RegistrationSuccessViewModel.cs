using System.Threading.Tasks;

namespace NottCS.ViewModels
{
    internal class RegistrationSuccessViewModel : BaseViewModel
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

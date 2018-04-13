using System.Windows.Input;
using NottCS.Services.Navigation;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    internal class EventRegistrationSuccessViewModel : BaseViewModel
    {
        public ICommand OkTapped => new Command(async () => { await NavigationService.BackUntilAsync<HomeViewModel>(); });
    }
}

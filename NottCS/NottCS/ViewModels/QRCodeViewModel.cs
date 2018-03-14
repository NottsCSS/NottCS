using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Services.Navigation;
using NottCS.Views;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    class QRCodeViewModel : BaseViewModel
    {
        public ICommand Command1 => new Command(async () => await func1());

        private async Task func1()
        {
            await NavigationService.BackUntilAsync<HomeViewModel>();
        }
    }
}

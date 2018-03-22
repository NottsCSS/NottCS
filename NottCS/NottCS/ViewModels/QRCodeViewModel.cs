using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Services.Navigation;
using Xamarin.Forms;
using ZXing.Common;


namespace NottCS.ViewModels
{
    internal class QRCodeViewModel : BaseViewModel
    {
        //public ICommand Command1 => new Command(async () => await func1());

        private async Task func1()
        {
            await NavigationService.BackUntilAsync<HomeViewModel>();
        }

        public EncodingOptions BarcodeOption => new EncodingOptions() { Height = 300, Width = 300, PureBarcode = true };
    }
}

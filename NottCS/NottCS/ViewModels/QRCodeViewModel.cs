using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Services.Navigation;
using Xamarin.Forms;
using ZXing.Common;
using ZXing.Net.Mobile.Forms;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace NottCS.ViewModels
{
    internal class QRCodeViewModel : BaseViewModel
    {

        //public ICommand Command1 => new Command(async () => await func1());
        public ICommand ButtonOnClicked => new Command(async() => await (scanner()));

        public async Task scanner()
        {
            var scannerPage = new ZXingScannerPage();
            var stack = Application.Current.MainPage.Navigation;
            await stack.PushAsync(scannerPage);

            scannerPage.OnScanResult += (result) =>
            {
                scannerPage.IsScanning = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await stack.PopAsync();
                    Acr.UserDialogs.UserDialogs.Instance.Alert(result.Text,"Scanned Detail", "OK");
                });
            };
        }
        private async Task func1()
        {
            await NavigationService.BackUntilAsync<HomeViewModel>();
        }

        public EncodingOptions BarcodeOption => new EncodingOptions() { Height = 500, Width = 500, PureBarcode = true };
    }
}

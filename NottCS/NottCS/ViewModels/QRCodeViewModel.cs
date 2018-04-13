using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Services.Navigation;
using Xamarin.Forms;
using ZXing.Common;
using ZXing.Net.Mobile.Forms;

namespace NottCS.ViewModels
{
    internal class QRCodeViewModel : BaseViewModel
    {
        public string GeneratedQRCodeText { get; set; }
        
        public ICommand ScanQRCode => new Command(async() => await (Scanner()));
        public async Task Scanner()
        {
            var scannerPage = new ZXingScannerPage();
            var nav = Application.Current.MainPage.Navigation;
            await nav.PushAsync(scannerPage);

            scannerPage.OnScanResult += (result) =>
            {
                scannerPage.IsScanning = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await nav.PopAsync();
                    Acr.UserDialogs.UserDialogs.Instance.Alert(result.Text,"Scanned Detail", "OK");
                });
            };
        }
        public EncodingOptions BarcodeOption => new EncodingOptions() { Height = 500, Width = 500, PureBarcode = true };
        //public ICommand Command1 => new Command(async () => await func1());
        private async Task func1()
        {
            await NavigationService.BackUntilAsync<HomeViewModel>();
        }

        public QRCodeViewModel()
        {
            GeneratedQRCodeText = "Just a bunch of text here";
            //todo Dyanmically generate QRCode with format.
        }
        
    }
}

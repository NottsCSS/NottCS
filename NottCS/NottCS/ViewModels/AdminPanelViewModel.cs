using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Services;
using NottCS.Services.Navigation;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace NottCS.ViewModels
{
    class AdminPanelViewModel:BaseViewModel
    {
        public class AdminPanelObject
        {
            public string FunctionName { get; set; }
            public string FucntionId { get; set; }
        }

        public ObservableCollection<AdminPanelObject> AdminPanel { get; set; } = new ObservableCollection<AdminPanelObject>()
            {
                new AdminPanelObject()
                {
                    FunctionName = "Create an event instance.",
                    FucntionId = "1"
                },
                new AdminPanelObject()
                {
                    FunctionName = "Verify a QR Code",
                    FucntionId = "2"
                }
            };

        public ICommand NavigationCommand => new Command(SwitchCaseNavigation);

        private void SwitchCaseNavigation(object p)
        {
            DebugService.WriteLine(p.ToString());
            if (p is AdminPanelObject something)
            {
                DebugService.WriteLine(something.FucntionId);
                switch (something.FucntionId)
                {
                    case "1":
                    {
                        CreateEventNavigate().GetAwaiter();
                    }
                        break;
                    case "2":
                    {
                        ScanQRCode().GetAwaiter();
                    }
                        break;
                }
                
            }
        }

        private async Task CreateEventNavigate()
        {
            try
            {
                await NavigationService.NavigateToAsync<CreateEventViewModel>();
                DebugService.WriteLine("Initiated navigation to CreateEventPage");
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e.Message);
            }
        }
        
        public async Task ScanQRCode()
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
                    Acr.UserDialogs.UserDialogs.Instance.Alert(result.Text, "Scanned Detail", "OK");
                });
            };
        }



        #region Disable ItemSelectedCommand
        public ICommand DisableItemSelectedCommand => new Command(DisableItemSelected);
        public void DisableItemSelected()
        {
        }
        #endregion

        #region AdminPanelViewModel Constructor

        public AdminPanelViewModel()
        {
            Title = "Admin Panel";
        }

        #endregion
    }
}

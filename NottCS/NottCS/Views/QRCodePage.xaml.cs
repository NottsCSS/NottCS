using System;
using System.Diagnostics;
using NottCS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace NottCS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QRCodePage : ContentPage
	{
		public QRCodePage ()
		{
			InitializeComponent ();
		    AddImage(PlaceHolder, "NottCS.Images.example-background.jpg");
            BindingContext = new QRCodeViewModel();
            //AddImage(Back, "NottCS.Images.Icons.back.png");
        }
	    private void AddImage(Image imageContainer, string imageLocation)
	    {
	        var assembly = typeof(NottCS.Views.LoginPage);
	        if (imageContainer != null)
	        {
	            imageContainer.Source = ImageSource.FromResource(imageLocation, assembly);
	        }
	    }


        private async void Button_OnClicked(object sender, EventArgs e)
	    {
	        var scannerPage = new ZXingScannerPage();
	        await Navigation.PushAsync(scannerPage);

	        scannerPage.OnScanResult += (result) =>
	        {
	            scannerPage.IsScanning = false;
	            Device.BeginInvokeOnMainThread(async () =>
	            {
	                await Navigation.PopAsync();
	                await DisplayAlert("Scanned Detail", result.Text, "OK");
	            });
	        };

	    }
    }
}
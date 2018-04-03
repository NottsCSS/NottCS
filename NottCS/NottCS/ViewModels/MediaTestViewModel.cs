using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace NottCS.ViewModels
{
    internal class MediaTestViewModel : BaseViewModel
    {
        private ImageSource _imgSrc;

        public MediaTestViewModel()
        {
            Title = "Media Test Page";
        }
        public ICommand MediaPicker => new Command(MediaButtonClicked);

        public ImageSource ImgSrc
        {
            get => _imgSrc;
            set => SetProperty(ref _imgSrc, value);
        }

        private async void MediaButtonClicked()
        {
            
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                UserDialogs.Instance.Alert(":( Permission not granted to photos.", "Photos Not Supported", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

            });


            if (file == null)
                return;

            ImgSrc = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }
    }
}

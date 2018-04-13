using System.IO;
using System.Windows.Input;
using Plugin.Media;
using Xamarin.Forms;
using Acr.UserDialogs;
using NottCS.Services;
using NottCS.Services.REST;

namespace NottCS.ViewModels
{
    internal class MediaTestViewModel : BaseViewModel
    {
        private ImageSource _imgSrc;

        public MediaTestViewModel()
        {
            Title = "Media Test Page";
        }

        private byte[] _image = null;

        public string Name { get; set; }
        public string Description { get; set; }

        public ICommand MediaPicker => new Command(MediaButtonClicked);
        public ICommand Upload => new Command(UploadButtonClicked);

        public ImageSource ImgSrc
        {
            get => _imgSrc;
            set => SetProperty(ref _imgSrc, value);
        }
        private static byte[] ReadStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
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
                //                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 100
            });
            if (file == null)
                return;
            DebugService.WriteLine(file.Path);
            DebugService.WriteLine(file.ToString());

            ImgSrc = ImageSource.FromStream(file.GetStream);

            var stream = file.GetStream();
            _image = ReadStream(stream);
//            ImgSrc = ImageSource.FromStream(() =>
//            {
//                return stream;
//            });
        }

        private async void UploadButtonClicked()
        {
            if (_image == null)
                return;
            await RestService.RequestPostAsync2(_image, "file1.jpg", Name, Description);
        }
    }
}

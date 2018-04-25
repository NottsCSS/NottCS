using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using NottCS.Services;
using NottCS.Services.Navigation;
using NottCS.Services.REST;
using Plugin.Media;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    class CreateEventViewModel3:BaseViewModel
    {
        private ImageSource _imgSrc;

        private byte[] _image = null;
        public class EventTime
        {
            public DateTime StartDate;
            public DateTime EndDate;
            public DateTime StartTime;
            public DateTime EndTime;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EventName { get; set; }
        public ICommand MediaPicker => new Command(MediaButtonClicked);
        public ICommand Upload => new Command(UploadButtonClicked);
        public ICommand NextPageNavigation => new Command(async() => await(DoStuff()));
        public ICommand BindingDateandTimeCommand => new Command(BindingDateandTime);
        public List<EventTime> ListofDateandTime = new List<EventTime>()
        {
            new EventTime()
        };
        public void BindingDateandTime()
        {
            ListofDateandTime.Add(new EventTime());
        }

        public async Task DoStuff()
        {
            try
            {
                await NavigationService.NavigateToAsync<CreateEventViewModel>();
                DebugService.WriteLine("Initiated navigation to CreateEventPage");
                foreach (var stuff in ListofDateandTime)
                {
                    DebugService.WriteLine("Start Date"+stuff.StartDate);
                    DebugService.WriteLine("End Date" + stuff.EndDate);
                    DebugService.WriteLine("Start Time" + stuff.StartTime);
                    DebugService.WriteLine("End Time" + stuff.EndTime);

                }
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e.Message);
            }
        }
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
    

    public CreateEventViewModel3()
    {
        Title = "Create Event";
    }
    }
}

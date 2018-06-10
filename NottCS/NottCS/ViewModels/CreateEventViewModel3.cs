using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Newtonsoft.Json;
using NottCS.Models;
using NottCS.Services;
using NottCS.Services.Navigation;
using NottCS.Services.REST;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    class CreateEventViewModel3 : BaseViewModel
    {
        private ImageSource _imgSrc;

        private byte[] _image = null;

        private ObservableCollection<EventTime> _eventTimeList = new ObservableCollection<EventTime>()
        {
            new EventTime()
            {
                Id = "0",
                Event = "3", //TODO: to be determined based on this event id
                StartTime = DateTime.Now,
                EndTime = DateTime.Now + new TimeSpan(0, 2, 0, 0)
            }
        };
        //private DateTime testingDate;
        //public DateTime TestingDate
        //{
        //    get { return testingDate; }
        //    set
        //    {
        //        testingDate = value;
        //        OnPropertyChanged("TestingDate");   //Call INPC Interface when property changes, so the view will know it has to update
        //    }
        //}

        //private void ChangeDate(DateTime newDate)
        //{
        //    TestingDate = newDate;  //Assing your new date to your property
        //}
        public string Name { get; set; }
        public string Description { get; set; }
        public string EventName { get; set; }
        public ICommand MediaPicker => new Command(MediaButtonClicked);
        public ICommand Upload => new Command(UploadButtonClicked);
        public ICommand NextPageNavigation => new Command(async () => await (DoStuff()));
        public ICommand AddEventTimeCommand => new Command(AddEventTime);
        public ICommand PrintEventTimeCommand => new Command(PrintEventTime);

        public ICommand DisableItemSelectedCommand => new Command(() => { });

        public ObservableCollection<EventTime> EventTimeList
        {
            get => _eventTimeList;
            set => SetProperty(ref _eventTimeList, value);
        }

        public EventTime TestEventTime { get; set; } = new EventTime()
        {
            Id = "0",
            Event = "3",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now + new TimeSpan(0, 2, 0, 0)
        };
   

        private void AddEventTime()
        {
            EventTimeList.Add(new EventTime()
            {
                Id = "0",
                Event = "3", //TODO: change this event to be based on current event id
                StartTime = DateTime.Now,
                EndTime = DateTime.Now + new TimeSpan(0,2,0,0)
            });
        }

        private void PrintEventTime()
        {
            foreach (var eventTime in EventTimeList)
            {
                DebugService.WriteLine(eventTime.StartTime);
                DebugService.WriteLine(eventTime.EndTime);
            }
        }
        


        public async Task DoStuff()
        {
            try
            {
                await NavigationService.NavigateToAsync<CreateEventViewModel>();
                DebugService.WriteLine("Initiated navigation to CreateEventPage");
                //DebugService.WriteLine("Start Date   " + TestingDate);
                foreach (var eventDateTime in EventTimeList)
                {
                    var eventDateTimeJson = JsonConvert.SerializeObject(eventDateTime);
                    DebugService.WriteLine($"Event date time: {eventDateTimeJson}");
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

            MediaFile file = null;
            try
            {

                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(
                    new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        //                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                        MaxWidthHeight = 100
                    });
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e);
                Acr.UserDialogs.UserDialogs.Instance.Alert(e.Message,
                    "File Picker Exception", "OK");
            }
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Newtonsoft.Json;
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
            public DateTime StartDateTime;
            public DateTime EndDateTime;
        }
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
        public ICommand NextPageNavigation => new Command(async() => await(DoStuff()));
        public ICommand BindingDateandTimeCommand => new Command(BindingDateandTime);
        public ICommand DateTimeChangedCommand => new Command(DateTimeChanged);

        public ObservableCollection<EventTime> ListofDateandTime = new ObservableCollection<EventTime>()
        {
            new EventTime()
        };
        public void BindingDateandTime()
        {
            ListofDateandTime.Add(new EventTime());
        }

        public void DateTimeChanged(object data)
        {
            if (data is Tuple<int, object> dataTuple)
            {
                int pickerId = dataTuple.Item1;
                int index = pickerId / 4;
                object sender = dataTuple.Item2;
                DateTime previousStartDateTime = ListofDateandTime[index].StartDateTime;
                DateTime previousEndDateTime = ListofDateandTime[index].EndDateTime;
                switch (pickerId % 4)
                {
                    case 0: //Start Date
                        var newStartDate = ((DatePicker)sender).Date;
                        var previousStartTime = previousStartDateTime.TimeOfDay;
                        ListofDateandTime[index].StartDateTime = newStartDate.Add(previousStartTime);
                        DebugService.WriteLine($"New start dateTime: {ListofDateandTime[index].StartDateTime}");
                        break;
                    case 1: //Start Time
                        var newStartTime = ((TimePicker)sender).Time;
                        var previousStartDate = previousStartDateTime.Date;
                        ListofDateandTime[index].StartDateTime = previousStartDate.Add(newStartTime);
                        DebugService.WriteLine($"New start dateTime: {ListofDateandTime[index].StartDateTime}");
                        break;
                	case 2: //End Date
	                    var newEndDate = ((DatePicker)sender).Date;
	                    var previousEndTime = previousEndDateTime.TimeOfDay;
	                    ListofDateandTime[index].EndDateTime = newEndDate.Add(previousEndTime);
	                    DebugService.WriteLine($"New end dateTime: {ListofDateandTime[index].EndDateTime}");
                        break;
                	case 3: //End Time
	                    var newEndTime = ((TimePicker)sender).Time;
	                    var previousEndDate = previousEndDateTime.Date;
	                    ListofDateandTime[index].EndDateTime = previousEndDate.Add(newEndTime);
	                    DebugService.WriteLine($"New end dateTime: {ListofDateandTime[index].EndDateTime}");
                        break;
                }
            }
        }


        public async Task DoStuff()
        {
            try
            {
                await NavigationService.NavigateToAsync<CreateEventViewModel>();
                DebugService.WriteLine("Initiated navigation to CreateEventPage");
                //DebugService.WriteLine("Start Date   " + TestingDate);
                foreach (var eventDateTime in ListofDateandTime)
                {
                    var endDateTimeJson = JsonConvert.SerializeObject(eventDateTime.EndDateTime);
                    var startDateTimeJson = JsonConvert.SerializeObject(eventDateTime.StartDateTime);
                    DebugService.WriteLine($"Start date time: {startDateTimeJson}");
                    DebugService.WriteLine($"End date time: {endDateTimeJson}");
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

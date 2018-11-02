using NottCS.Services.Stuff;
using System;
using System.Diagnostics;
using System.Windows.Input;

using Xamarin.Forms;

namespace NottCS.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private readonly IStuffService _stuffService;
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public AboutViewModel(IStuffService stuffService)
        {
            _stuffService = stuffService;
            OpenWebCommand = new Command(() => _stuffService.SetStuff(100));
            Title = _stuffService.Name;
        }

        public string ASD { get; set; } = "AISCBCAS";

        public ICommand OpenWebCommand { get; }
    }
}
using NottCS.Services.Stuff;
using System;
using System.Diagnostics;
using System.Windows.Input;

using Xamarin.Forms;

namespace NottCS.ViewModels
{
    //This ViewModel is used purely to test out the dependency injection method using the ViewModelLocator pattern
    //Can be deleted once it is clear that the dependency injection is stable
    public class DITestViewModel : BaseViewModel
    {
        private readonly IStuffService _stuffService;

        public DITestViewModel(IStuffService stuffService)
        {
            _stuffService = stuffService;
            OpenWebCommand = new Command(() => _stuffService.SetStuff(100));
            Title = _stuffService.Name;
        }

        public string ASD { get; set; } = "AISCBCAS";

        public ICommand OpenWebCommand { get; }
    }
}
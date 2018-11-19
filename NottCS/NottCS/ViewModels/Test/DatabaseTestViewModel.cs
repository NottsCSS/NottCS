using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Services.Data.Club;
using NottCS.Services.Sync;
using Xamarin.Forms;

namespace NottCS.ViewModels.Test
{
    //This page is to
    //1) Test synchronisation performance
    //2) Test image post
    //3) Test synchronisation issues (local first or remote first, if remote fails then what, if local fails then what)
    public class DatabaseTestViewModel : BaseViewModel
    {
        private readonly IClubService _clubService;
        private readonly SyncService _syncService;

        private ObservableCollection<Models.Club> _clubList;
        private string _message;

        public DatabaseTestViewModel(IClubService clubService, SyncService syncService)
        {
            IsBusy = true;
            _clubService = clubService;
            _syncService = syncService;
            InitializeAsync(null);
        }

        public override async Task InitializeAsync(object navigationData)
        {
            ClubList = new ObservableCollection<Models.Club>(await _clubService.GetAllClubsAsync());
            IsBusy = false;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public ICommand SubmitCommand => new Command(async () => await Submit());

        private async Task Submit()
        {
            IsBusy = true;
            try
            {
                var club = new Models.Club()
                {
                    Name = this.Name,
                    Description = this.Description,
                    IconUrl = this.IconUrl
                };
                await _clubService.AddClubAsync(club);
                ClubList = new ObservableCollection<Models.Club>(await _clubService.GetAllClubsAsync());
                await _syncService.Sync();
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public ObservableCollection<Models.Club> ClubList
        {
            get => _clubList;
            set => SetProperty(ref _clubList, value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using NottCS.Services.Data;
using NottCS.Services.Data.Club;
using NottCS.Services.Data.Member;
using NottCS.Services.Data.User.NottCS.Services.Data.Club;

namespace NottCS.ViewModels.Club
{
    public class ClubListViewModel:BaseViewModel
    {
        private readonly IClubService _clubService;
        private readonly IMemberService _memberService;
        private readonly IUserService _userService;
        public ClubListViewModel(IClubService clubService, IMemberService memberService, IUserService userService)
        {
            _clubService = clubService;
            _memberService = memberService;
            _userService = userService;
            SelectedClubTypeIndex = 1;
            var task = InitializeAsync();
        }

        //This InitializeAsync is not of the base class, this is a local implementation
        private async Task InitializeAsync()
        {
            AllClubList = new ObservableCollection<Models.Club>(await _clubService.GetAllClubsAsync());
            var userId = (await _userService.GetUser()).Id;
            Debug.WriteLine($"User id is :{userId}");
            var memberList = await _memberService.GetMemberByUserId(userId);
            foreach (var member in memberList)
            {
                Debug.WriteLine($"Member id: {member.Id}, adding club by id :{member.ClubId}");
                MyClubList.Add(await _clubService.GetClubByIdAsync(member.ClubId));
            }
            SelectedClubTypeIndex = 1;
        }

        public List<string> ClubListTypePickerList { get; set; } = new List<string> { "My Clubs Only", "All Clubs" };
        private int _selectedClubTypeIndex;
        public int SelectedClubTypeIndex
        {
            get => _selectedClubTypeIndex;
            set
            {
                _selectedClubTypeIndex = value;
                switch (value)
                {
                    case 0:
                        ClubList = MyClubList;
                        Debug.WriteLine("ClubList changed to MyClubList");
                        break;
                    case 1:
                        ClubList = AllClubList;
                        Debug.WriteLine("ClubList changed to AllClubList");
                        break;
                }
            }
        }
        private ObservableCollection<Models.Club> _clubList = new ObservableCollection<Models.Club>();
        public ObservableCollection<Models.Club> ClubList
        {
            get => _clubList;
            set => SetProperty(ref _clubList, value);
        }

#region MockData

        private ObservableCollection<Models.Club> _allClubList = new ObservableCollection<Models.Club>();
        private ObservableCollection<Models.Club> _myClubList = new ObservableCollection<Models.Club>();
        private ObservableCollection<Models.Club> AllClubList
        {
            get => _allClubList;
            set => SetProperty(ref _allClubList, value);
        }


        private ObservableCollection<Models.Club> MyClubList
        {
            get => _myClubList;
            set => SetProperty(ref _myClubList, value);
        }

        #endregion


    }
}

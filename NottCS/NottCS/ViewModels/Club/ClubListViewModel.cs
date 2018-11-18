using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace NottCS.ViewModels.Club
{
    public class ClubListViewModel:BaseViewModel
    {
        public ClubListViewModel()
        {
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
        private ObservableCollection<Models.Club> AllClubList { get; set; } = new ObservableCollection<Models.Club>
        {
            new Models.Club()
            {
                Id = "0",
                Name = "Test1",
                Description = "Loren Ipsum"
            },
            new Models.Club()
            {
                Id = "1",
                Name = "Test2",
                Description = "Loren Gypsum"
            }
        };
        private ObservableCollection<Models.Club> MyClubList { get; set; } = new ObservableCollection<Models.Club>()
        {
            new Models.Club()
            {
                Id = "0",
                Name = "TestMy1",
                Description = "Loren Ipsum"
            },
            new Models.Club()
            {
                Id = "1",
                Name = "TestMy2",
                Description = "Loren Blek"
            }
        };


        #endregion


    }
}

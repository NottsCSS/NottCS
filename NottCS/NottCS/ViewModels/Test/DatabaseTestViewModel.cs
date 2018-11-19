using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NottCS.ViewModels.Test
{
    public class DatabaseTestViewModel : BaseViewModel
    {
        public ObservableCollection<Models.Club> ClubList { get; set; }
    }
}

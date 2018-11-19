using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NottCS.ViewModels.Test
{
    public class DatabaseTestViewModel : BaseViewModel
    {
        public ObservableCollection<Services.Data.Models.ClubData> ClubList { get; set; }
    }
}

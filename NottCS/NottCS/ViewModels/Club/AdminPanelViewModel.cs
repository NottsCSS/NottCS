using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NottCS.ViewModels.Club
{
    public class AdminPanelViewModel:BaseViewModel
    {
        public class AdminPanelObject
        {
            public string FunctionName { get; set; }
            public string FucntionId { get; set; }
        }
        public ObservableCollection<AdminPanelObject> AdminPanel { get; set; } = new ObservableCollection<AdminPanelObject>()
        {
            new AdminPanelObject()
            {
                FunctionName = "Create an event instance.",
                FucntionId = "1"
            },
            new AdminPanelObject()
            {
                FunctionName = "Verify a QR Code",
                FucntionId = "2"
            }
        };
    }
}

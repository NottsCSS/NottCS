using System.Windows.Input;
using NottCS.Services;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    internal class ReportIssueViewModel : BaseViewModel
    {
        public ICommand ReportIssueCommand => new Command(ReportIssue);

        private void ReportIssue()
        {
            DebugService.WriteLine("Report Issue Function Called");
        }
    }


}
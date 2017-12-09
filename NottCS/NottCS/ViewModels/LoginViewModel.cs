using System;
using System.Text.RegularExpressions;
using System.Windows.Input;

using Xamarin.Forms;

namespace NottCS.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            Title = "NottCS Login";
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                Regex r = new Regex("^[a-zA-Z0-9]*$");
                _userName = value;
                if (r.IsMatch(value))
                {
                }
                else
                {

                }
            }
        }
        public ICommand OpenWebCommand { get; }
    }
}
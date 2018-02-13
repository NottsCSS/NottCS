using NottCS.Models;
using NottCS.Services.REST;
using NottCS.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using NottCS.Services.Navigation;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    internal class AccountViewModel : BaseViewModel
    {
        #region ViewModalAdditionalClass

        public class UserDataObject
        {
            public string DataName { get; set; }
            public string DataValue { get; set; }
        }

        #endregion

        #region PageProperties
        private User _loginUser;
        private List<UserDataObject> _dataList;

        public User LoginUser
        {
            get => _loginUser;
            set => SetProperty(ref _loginUser, value);
        }

        public List<UserDataObject> DataList
        {
            get => _dataList;
            set => SetProperty(ref _dataList, value);
        }

        #endregion

        /// <summary>
        /// Constructor of AccountViewModel
        /// </summary>
        public AccountViewModel()
        {
            Title = "Account Page";
        }

        /// <summary>
        /// Sets the data for the page
        /// </summary>
        /// <param name="username">Username for the account data</param>
        private async Task<bool> SetPageDataAsync(string username)
        {
            var respondData = await BaseRestService.RequestGetAsync<User>(username);

            if (!respondData.Item1)
            {
                //TODO: Implement error notification
                UserDialogs.Instance.Alert("We're not able to log you in. Please try again.", "Login Error", "Ok");
                return false;
            }
            else
            {
                LoginUser = respondData.Item2;
                DataList = new List<UserDataObject>()
                {
                    new UserDataObject(){DataName = "Name", DataValue = LoginUser.Name},
                    new UserDataObject(){DataName = "Username", DataValue = LoginUser.Username},
                    new UserDataObject(){DataName = "Student ID", DataValue = LoginUser.StudentId},
                    new UserDataObject(){DataName = "Library Number", DataValue = LoginUser.LibraryNumber},
                    new UserDataObject(){DataName = "Studying Course", DataValue = LoginUser.Course}
                };
                return true;
            }
        }

        /// <summary>
        /// Initializes the page
        /// </summary>
        /// <param name="navigationData">Data passed from the previous page</param>
        /// <returns></returns>
        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is string username)
            {
                var isSuccess = SetPageDataAsync(username).GetAwaiter().GetResult();
                if (isSuccess)
                {
                    return base.InitializeAsync(navigationData);
                }
            }

            return null;
        }
    }
}

using NottCS.Models;
using NottCS.Services.REST;
using NottCS.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        /// Sets the data of the page
        /// </summary>
        /// <param name="navigationData">Navigation Data</param>
        private void SetPageData(object navigationData)
        {
            //LoginUser = UserService.GetUserByUsername(username).GetAwaiter().GetResult();
            LoginUser = new User() { Username = "kecy6cyt", Name = "Cheow Yeu Tyng", LibraryNumber = "2001434962", Password = "123", StudentId = "18816756", Course = "Electrical & Electronics Engineering" };
            DataList = new List<UserDataObject>()
            {
                new UserDataObject(){DataName = "Name", DataValue = LoginUser.Name},
                new UserDataObject(){DataName = "Username", DataValue = LoginUser.Username},
                new UserDataObject(){DataName = "Student ID", DataValue = LoginUser.StudentId},
                new UserDataObject(){DataName = "Library Number", DataValue = LoginUser.LibraryNumber},
                new UserDataObject(){DataName = "Studying Course", DataValue = LoginUser.Course}
            };
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
                SetPageData(navigationData);
            }
            return base.InitializeAsync(navigationData);
        }
    }
}

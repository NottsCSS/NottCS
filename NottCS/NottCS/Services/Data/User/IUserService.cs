using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NottCS.Models;

namespace NottCS.Services.Data.User
{
    namespace NottCS.Services.Data.Club
    {
        public interface IUserService
        {
            Task<Models.User> GetUser();
            Task UpdateUser(Models.User user);
        }
    }
}

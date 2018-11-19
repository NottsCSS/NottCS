using System;
using System.Collections.Generic;
using System.Text;
using NottCS.Services.Data.Models;

namespace NottCS.Services.Data
{
    public interface IClubService
    {
        IEnumerable<Models.ClubData> GetAllClubs();
    }
}

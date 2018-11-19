using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NottCS.Models;

namespace NottCS.Services.Data.Club
{
    public interface IClubService
    {
        Task<List<Models.Club>> GetAllClubsAsync();
    }
}

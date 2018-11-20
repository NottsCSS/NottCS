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
        Task<List<Models.Club>> GetMyClubsAsync();
        Task<Models.Club> GetClubByIdAsync(int id);
        Task<Models.Club> GetClubByNameAsync(string name);
        Task AddClubAsync(Models.Club club);
        Task DeleteClubAsync(Models.Club club);
        Task DeleteClubAsync(int id);
    }
}

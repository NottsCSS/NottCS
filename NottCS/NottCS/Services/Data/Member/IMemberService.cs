using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NottCS.Services.Data.Member
{
    public interface IMemberService
    {
        Task<List<Models.Member>> GetAllMembersAsync();
        Task<List<Models.Member>> GetMemberByUserId(int userId);
        Task<List<Models.Member>> GetMemberByStatusAsync(Models.MemberStatus status);
        Task<List<Models.Member>> GetMemberByClubIdAsync(int clubId);
        Task AddMemberAsync(Models.Member member);
        Task UpdateMemberAsync(Models.Member member);
        Task DeleteMemberAsync(Models.Member member);
        Task DeleteMemberAsync(int memberId);
    }
}

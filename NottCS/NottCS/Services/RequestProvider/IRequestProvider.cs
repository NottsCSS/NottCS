using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NottCS.Models.Request;

namespace NottCS.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<RequestResult<TData>> GetDataAsync<TData>(string url, string token);
        Task<RequestResult<TData>> PostDataAsync<TData>(string url, TData requestBody, string token);
        Task<RequestResult<TData>> PutDataAsync<TData>(string url, TData requestBody, string token);
        Task<RequestResult<TData>> DeleteData<TData>(string url, string token);
    }
}

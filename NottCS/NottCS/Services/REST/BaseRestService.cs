using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NottCS.Services.REST
{
    internal class BaseRestService
    {
        //TODO: Update the Uri when the domain name is available
        private const string BaseAddress = "https://apis.nottcs.com/";

        //TODO: Setup client with proper headers
        protected static readonly HttpClient Client = new HttpClient();

        protected enum ServiceType
        {
            GetUserByUsername, CreateUser, UpdateUserByUsername, DeleteUserByUsername
        }

        /// <summary>
        /// Generates appropriate type of REST Uri for requests
        /// </summary>
        /// <param name="service">Request Type</param>
        /// <param name="identifier">Identifier for search query, usually the primary key</param>
        /// <returns></returns>
        protected static string UriGenerator(ServiceType service, string identifier = null)
        {
            var returnUri = "";

            //TODO: Update the Uri when the documentation from the backend is ready
            switch (service)
            {
                case ServiceType.CreateUser:
                {
                    returnUri = BaseAddress + "/user/createUser";
                    break;
                }
                case ServiceType.DeleteUserByUsername:
                {
                    returnUri = BaseAddress + "/user/deleteUserByUsername/" + identifier;
                    break;
                }
                case ServiceType.GetUserByUsername:
                {
                    returnUri = BaseAddress + "/user/getUserByUsername/" + identifier;
                    break;
                }
                case ServiceType.UpdateUserByUsername:
                {
                    returnUri = BaseAddress + "/user/updateUserByUsername/" + identifier;
                    break;
                }
                default:
                {
                    returnUri = BaseAddress + "/apiNotFound";
                    break;
                }
            }

            return returnUri;
        }
    }
}

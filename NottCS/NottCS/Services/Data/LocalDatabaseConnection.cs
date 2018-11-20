using SQLite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Text;
using NottCS.Models;

namespace NottCS.Services.Data
{
    public class LocalDatabaseConnection : SQLiteAsyncConnection
    {

        private static readonly string DbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "NottCSData.db3");
        public LocalDatabaseConnection() : base(DbPath, storeDateTimeAsTicks:false, 
            openFlags: SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex)
        {
            CreateTableAsync<Models.Club>().Wait();
            CreateTableAsync<Models.Member>().Wait();
            CreateTableAsync<Models.User>().Wait();
        }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Text;
using NottCS.Models;

namespace NottCS.Services.Data
{
    public class LocalDatabaseService : SQLiteAsyncConnection
    {

        private static readonly string DbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "NottCSData.db3");
        public LocalDatabaseService() : base(DbPath, storeDateTimeAsTicks:false)
        {
            CreateTableAsync<Models.Club>().Wait();
        }
    }
}

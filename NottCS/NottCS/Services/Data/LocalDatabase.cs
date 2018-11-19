using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using NottCS.Services.Data.Models;

namespace NottCS.Services.Data
{
    internal class LocalDatabase
    {
        private readonly SQLiteAsyncConnection _database;
        public LocalDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Club>().Wait();
        }

        public void GetStuffAsync()
        {
        }
    }
}

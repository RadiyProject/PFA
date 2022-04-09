using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace PFA.Database
{
    public class Database_product
    {
        readonly SQLiteAsyncConnection _database;
        public Database_product(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Product>().Wait();
        }
    }
}

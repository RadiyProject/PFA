using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace PFA.Database
{
    public class DBGood
    {
        readonly SQLiteAsyncConnection database;
        public DBGood(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Good>().Wait();
        }
        public Task<List<Good>> GetAsync()
        {
            return database.Table<Good>().ToListAsync();
        }
        public Task<int> Create(Good good)
        {
            return database.InsertAsync(good);
        }
        public Task<int> Delete(Good good)
        {
            return database.DeleteAsync(good);
        }
    }
}

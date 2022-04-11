using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace PFA.Database
{
    public class DBCheque
    {
        readonly SQLiteAsyncConnection database;
        public DBCheque(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Cheque>().Wait();
        }
        public Task<List<Cheque>> GetAsync()
        {
            return database.Table<Cheque>().ToListAsync();
        }
        public Task<int> Create(Cheque cheque)
        {
            return database.InsertAsync(cheque);
        }
        public Task<int> Delete(Cheque cheque)
        {
            return database.DeleteAsync(cheque);
        }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PFA.Database
{
    public class DBBudget
    {
        readonly SQLiteAsyncConnection database;
        public DBBudget(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Budget>().Wait();
        }
        public Task<List<Budget>> GetAsync()
        {
            return database.Table<Budget>().ToListAsync();
        }
        public Task<List<Budget>> GetWithIdAsync(int id)
        {
            return database.Table<Budget>().Where(p => p.id == id).ToListAsync();
        }
        public Task<int> Create(Budget budget)
        {
            return database.InsertAsync(budget);
        }
        public Task<int> Update(Budget budget)
        {
            return database.UpdateAsync(budget);
        }
        public Task<int> Delete(Budget budget)
        {
            return database.DeleteAsync(budget);
        }
    }
}

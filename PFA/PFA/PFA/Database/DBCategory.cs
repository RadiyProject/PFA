using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PFA.Database
{
    public class DBCategory
    {
        readonly SQLiteAsyncConnection database;
        public DBCategory(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Category>().Wait();
        }
        public Task<List<Category>> GetAsync()
        {
            return database.Table<Category>().ToListAsync();
        }
        public Task<List<Category>> GetWithIdAsync(int id)
        {
            return database.Table<Category>().Where(p => p.id == id).ToListAsync();
        }
        public Task<int> Create(Category category)
        {
            return database.InsertAsync(category);
        }
        public Task<int> Update(Category category)
        {
            return database.UpdateAsync(category);
        }
        public Task<int> Delete(Category category)
        {
            return database.DeleteAsync(category);
        }
    }
}

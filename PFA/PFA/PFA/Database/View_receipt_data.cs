using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace PFA.Database
{
    public class Database_receipt
    {
        readonly SQLiteAsyncConnection _database;
        public Database_receipt(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Receipt>().Wait();
        }
        public Task<List<Receipt>> GetReceiptAsync()
        {
            return _database.Table<Receipt>().ToListAsync();
        }
        public Task<int> CreateReceipt(Receipt receipt)
        {
            return _database.InsertAsync(receipt);
        }
        public Task<int> DeleteReceipt(Receipt receipt)
        {
            return _database.DeleteAsync(receipt);
        }

    }
}

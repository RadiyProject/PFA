using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PFA.Database
{
    class DBGoodsForCheque
    {
        readonly SQLiteAsyncConnection database;
        public DBGoodsForCheque(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<GoodsForCheque>().Wait();
        }
        public Task<List<GoodsForCheque>> GetAsync()
        {
            return database.Table<GoodsForCheque>().ToListAsync();
        }
        public Task<int> Create(GoodsForCheque goodsForCheque)
        {
            return database.InsertAsync(goodsForCheque);
        }
        public Task<int> Delete(GoodsForCheque goodsForCheque)
        {
            return database.DeleteAsync(goodsForCheque);
        }
    }
}

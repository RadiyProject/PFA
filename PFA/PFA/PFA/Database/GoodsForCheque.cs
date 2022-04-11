using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFA.Database
{
    public class GoodsForCheque
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public int amount { get; set; }

        public GoodsForCheque()
        {
            amount = 0;
        }
        public GoodsForCheque(string name, float price, int amount)
        {
            this.name = name;
            this.price = price;
            this.amount = amount;
        }
    }
}

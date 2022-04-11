using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace PFA.Database
{
    public class Good
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string priceText { get; set; }
        public Good()
        {
            name = "Новый чек";
            price = 0;
        }
        public Good(string name, float price)
        {
            this.name = name;
            this.price = price;
        }
        public string GetTextPrice()
        {
            priceText = price + "р.";
            return priceText;
        }
    }
}

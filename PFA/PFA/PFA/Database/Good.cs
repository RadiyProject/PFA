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
        public string nameWithPrice { get; set; }
        public float price { get; set; }
        public string priceText { get; set; }
        public Good()
        {
        }
        public Good(string name, float price)
        {
            this.name = name;
            this.price = price;
            GetTextPrice();
        }
        public string GetTextPrice()
        {
            priceText = price + "р.";
            nameWithPrice = name + " " + priceText;
            return priceText;
        }
    }
}

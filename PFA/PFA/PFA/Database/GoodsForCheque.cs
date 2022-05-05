using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFA.Database
{
    public class GoodsForCheque
    {
        public string name { get; set; }
        public float price { get; set; }
        public string priceText { get; set; }
        public string nameWithPrice { get; set; }
        public int amount { get; set; }
        public string amountText { get; set; }
        public int id { get; set; }
        public int category { get; set; }
        public GoodsForCheque()
        {
            name = "Новый чек";
            price = 0;
            GetTextPrice();
        }
        public GoodsForCheque(string name, float price, int amount, int id, int category)
        {
            this.name = name;
            this.price = price;
            this.amount = amount;
            this.id = id;
            this.category = category;
            GetTextPrice();
        }
        public void IncreaseAmount()
        {
            amount++;
            GetTextPrice();
        }
        public void DecreaseAmount()
        {
            amount--;
            GetTextPrice();
        }
        public string GetTextPrice()
        {
            priceText = price * amount + "р.";
            amountText = "x" + amount;
            nameWithPrice = name + " " + priceText;
            return priceText;
        }
    }
}

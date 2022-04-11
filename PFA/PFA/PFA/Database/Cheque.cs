using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace PFA.Database
{
    public class Cheque
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }
        public string dateText { get; set; }
        public float totalPrice { get; set; } = 0;
        public string totalPriceText { get; set; }
        public List<Good> goods = new List<Good>();
        public List<int> amounts = new List<int>();
        public Cheque()
        {
            name = "Новый чек";
            date = DateTime.Today;
            dateText = date.ToString("d");
            GetTextPrice();
        }
        public Cheque(string name, DateTime date)
        {
            this.name = name;
            this.date = date;
            dateText = date.ToString("d");
        }
        public void CalculatePrice()
        {
            int i = 0;
            foreach (Good currentGood in goods)
            {
                int currentAmount = amounts[i];
                totalPrice += (currentAmount * currentGood.price);
                i++;
            }
        }
        public string GetTextPrice()
        {
            totalPriceText = totalPrice + "р.";
            return totalPriceText;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public bool isOpened { get; set; }
        public bool isClosed { get; set; }
        public float colFirst { get; set; }
        public float colSecond { get; set; }
        public float colThird { get; set; }
        public int category { get; set; }
        public string userId { get; set; }
        [Ignore]
        public List<Category> allCategories
        {
            get
            {
                return Task.Run(() => App.Categories.GetAsync()).Result;
            }
        }
        [Ignore]
        public string selected
        {
            get
            {
                if (Task.Run(() => App.Categories.GetWithIdAsync(category)).Result.Count > 0)
                    return Task.Run(() => App.Categories.GetWithIdAsync(category)).Result.Last().name;
                else
                    return "Без категории";
            }
        }
        public Good()
        {
        }
        public Good(string name, float price, int category)
        {
            this.name = name;
            this.price = price;
            this.category = category;
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

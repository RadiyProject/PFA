using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync.Extensions;

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
        public string userId { get; set; }
        [Ignore]
        public List<GoodsForCheque> allGoods { get; set; }
        /*{
            get
            {
                List<GoodsForCheque> all = new List<GoodsForCheque>();
                List<Good> good = Task.Run(() => App.Goods.GetAsync()).Result;
                if (good != null && good.Count > 0)
                    foreach (Good temp in good)
                        all.Add(new GoodsForCheque(temp.name, temp.price, 1, id, temp.category));
                return all;
            }
        }*/
        [Ignore]
        public List<GoodsForCheque> goods { get; set; } = new List<GoodsForCheque>();
        public List<GoodsForCheque> goodsN {
            get
            {
                if (goodsString != null)
                    return JsonConvert.DeserializeObject<List<GoodsForCheque>>(goodsString);
                else
                    return null;
            }
        }
        public string goodsString { get; set; }
        public bool isOpened { get; set; }
        public bool isClosed { get; set; }
        public float colFirst { get; set; }
        public float colSecond { get; set; }
        public float colThird { get; set; }
        public float colFourth { get; set; }
        public void SetGoods()
        {
            goodsString = JsonConvert.SerializeObject(goods);
        }

        public Cheque()
        {
        }
        public Cheque(string name, DateTime date)
        {
            this.name = name;
            this.date = date;
            dateText = date.ToString("d");
            CalculatePrice();
        }
        public void CalculatePrice()
        {
            if (goods != null && goods.Count > 0)
            {
                totalPrice = 0;
                for (int i = 0; i < goods.Count; i++)
                {
                    if (goods[i].amount > 0)
                        totalPrice += goods[i].price * goods[i].amount;
                    else
                        goods.Remove(goods[i]);
                }
            }
            GetTextPrice();
        }
        public string GetTextPrice()
        {
            totalPriceText = totalPrice + "р.";
            return totalPriceText;
        }
        public void GetGoods(string goods)
        {
            if (goods != null)
                this.goods = (List<GoodsForCheque>)JsonConvert.DeserializeObject(goods);
        }
    }
}

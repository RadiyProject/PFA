using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFLibraryForTP
{
    
    [DataContract]
    public class GoodsForCheque
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public float price { get; set; }
        [DataMember]
        public string priceText { get; set; }
        [DataMember]
        public string nameWithPrice { get; set; }
        [DataMember]
        public int amount { get; set; }
        [DataMember]
        public string amountText { get; set; }
        [DataMember]
        public int id { get; set; } //id of cheque
        [DataMember]
        public int category { get; set; }
        [DataMember] [Key]
        public int idGFC { get; set; }

        public GoodsForCheque() 
        {
            amountText = "x" + amount;
            priceText = price * amount + "р.";
        }

        public GoodsForCheque(GoodS goodS)
        {
            name = goodS.goodName;
            price = goodS.price;
            nameWithPrice = goodS.nameWithPrice;
            amount = 1;
            amountText = "x" + amount;
            priceText = price * amount + "р.";
            category = goodS.category;
        }
    }
}
